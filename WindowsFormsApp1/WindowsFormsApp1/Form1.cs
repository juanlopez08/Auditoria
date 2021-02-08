using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.Funciones;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DatabaseConnection db = new DatabaseConnection();
        public Form1()
        {
            InitializeComponent();
            btnAnomaliasDatos.Enabled = false;
            btnRelaciones.Enabled = false;
            btnAnomaliasIntegridad.Enabled = false;
        }

        // VARIABLES GLOBALES
        string nombreBD;
        string user;
        string password;
        bool connected;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        void testConnection()
        {
            string[] args = { tbNombre.Text, tbUser.Text, tbPassword.Text };
            connected = db.connect(args);
            if (connected)
            {
                MessageBox.Show("Conexión establecida exitosamente!");
                btnAnomaliasDatos.Enabled = true;
                btnRelaciones.Enabled = true;
                btnAnomaliasIntegridad.Enabled = true;
            }
            else
                MessageBox.Show("Conexión fallida, verifique los datos ingresados.");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            testConnection();

            // SOLO ESTAN PARA PROBAR LOS BOTONES
            //btnAnomaliasDatos.Enabled = true;
            //btnRelaciones.Enabled = true;
            //btnAnomaliasIntegridad.Enabled = true;

        }

        private void tbNombre_TextChanged(object sender, EventArgs e)
        {
            nombreBD = tbNombre.Text;
        }

        private void btnRelaciones_Click(object sender, EventArgs e)
        {
            getRelaciones();   
        }

        /*Función para obtener las relaciones que existen y deberían existir.*/
        void getRelaciones()
        {
            db.openConnection();
            string query = @"DROP TABLE IF EXISTS PKRelations;
                    CREATE TABLE PKRelations (
	                    MainTable varchar(max),
	                    MainPK varchar(max),
	                    SecondaryTable varchar(max),
	                    SecondaryColumn varchar(max)
                    );
                    DECLARE @currentTable AS varchar(max), @currentPK as varchar(max)
                    DECLARE PrimaryCursor CURSOR FOR
                    SELECT DISTINCT I.TABLE_NAME, I.COLUMN_NAME
                    FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE I 
                    INNER JOIN sys.key_constraints S 
                    ON I.CONSTRAINT_NAME = S.name AND I.TABLE_NAME != 'sysdiagrams';
                    OPEN PrimaryCursor
                    FETCH NEXT FROM PrimaryCursor INTO @currentTable , @currentPK 
                    WHILE @@fetch_status = 0
                    BEGIN
                    INSERT INTO PKRelations
                    SELECT @currentTable AS 'MainTable', @currentPK as 'MainPK', TABLE_NAME AS 'SecondaryTable', COLUMN_NAME AS 'SecondaryColumn'
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME != 'sysdiagrams' AND TABLE_NAME != @currentTable AND COLUMN_NAME=@currentPK;
                    FETCH NEXT FROM PrimaryCursor INTO @currentTable, @currentPK
                    END
                    CLOSE PrimaryCursor
                    DEALLOCATE PrimaryCursor
                    DROP TABLE IF EXISTS CheckTriggers;
                    CREATE TABLE CheckTriggers (
	                    MainTable varchar(max),
	                    SecondaryTable varchar(max),
	                    TargetColumn varchar(max)
                    );
                    DECLARE @currentMain AS varchar(max), @currentSecondary as varchar(max), @currentColumn as varchar(max)
                    DECLARE PossibleRelationCursor CURSOR FOR
                    SELECT MainTable, SecondaryTable, MainPK FROM PKRelations;
                    OPEN PossibleRelationCursor
                    FETCH NEXT FROM PossibleRelationCursor INTO @currentMain, @currentSecondary, @currentColumn
                    WHILE @@fetch_status = 0
                    BEGIN
	                    DECLARE @currentFather AS varchar(max), @currentChild as varchar(max), @currentFK as varchar(max), @relationExists as varchar(max)
	                    SET @relationExists = 0;
	                    DECLARE RelationCursor CURSOR FOR
	                    SELECT OBJECT_NAME(referenced_object_id) AS 'Father', OBJECT_NAME(parent_object_id) AS 'Child', name from sys.foreign_keys;
	                    OPEN RelationCursor
	                    FETCH NEXT FROM RelationCursor INTO @currentFather, @currentChild, @currentFK
	                    WHILE @@FETCH_STATUS = 0
	                    BEGIN
		                    IF (@currentMain = @currentFather AND @currentSecondary = @currentChild) OR (@currentMain = @currentChild AND @currentSecondary = @currentFather)
			                    BEGIN
				                    SET @relationExists = 1;
				                    DECLARE @currentConstraintName as varchar(max), @currentConstraintColumn as varchar(max), @constraintExists as bit
				                    SET @constraintExists = 0;
				                    DECLARE ConstraintCursor CURSOR FOR
				                    SELECT CONSTRAINT_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE;
				                    OPEN ConstraintCursor 
				                    FETCH NEXT FROM ConstraintCursor  INTO @currentConstraintName, @currentConstraintColumn
				                    WHILE @@FETCH_STATUS = 0
				                    BEGIN
					                    IF (@currentColumn = @currentConstraintColumn AND @currentFK = @currentConstraintName)
						                    BEGIN
							                    SET @constraintExists = 1;
                                                PRINT('Existe relación entre '+@currentMain+' y '+@currentSecondary+' con la columna '+@currentColumn+'.');
							                    PRINT('Existe relación correspondiente para la columna '+@currentColumn+'. Su constraint es: '+@currentConstraintName);
                                                PRINT('')
						                    END
					                    FETCH NEXT FROM ConstraintCursor  INTO @currentConstraintName, @currentConstraintColumn
				                    END
				                    IF(@constraintExists != 1)
					                    BEGIN
						                    INSERT INTO CheckTriggers VALUES (@currentMain, @currentSecondary, @currentColumn);
						                    PRINT('Hay una coincidencia entre '+@currentMain+' y '+@currentSecondary+' con la columna '+@currentColumn+ ', existe la relación entre ambas tablas pero no existe ningún constraint correspondiente a esa columna. Se revisarán triggers.');
                                            PRINT('')
					                    END
				                    CLOSE ConstraintCursor
				                    DEALLOCATE ConstraintCursor
			                    END
	                    FETCH NEXT FROM RelationCursor INTO @currentFather, @currentChild, @currentFK
	                    END
	                    IF(@relationExists != 1)
		                    BEGIN
			                    INSERT INTO CheckTriggers VALUES (@currentMain, @currentSecondary, @currentColumn);
			                    PRINT('Hay una coincidencia entre '+@currentMain+' y '+@currentSecondary+' con la columna '+@currentColumn+ ' pero no existe ninguna relación entre ambas tablas. Se revisarán triggers.');
		                    END
	                    CLOSE RelationCursor
	                    DEALLOCATE RelationCursor
                    FETCH NEXT FROM PossibleRelationCursor INTO @currentMain, @currentSecondary, @currentColumn
                    END
                    CLOSE PossibleRelationCursor
                    DEALLOCATE PossibleRelationCursor
                    SELECT * FROM PKRelations";
            SqlCommand cmd = new SqlCommand(query, db.getConnection());
            string message = "";
            db.getConnection().InfoMessage += delegate (object sender, SqlInfoMessageEventArgs e)
            {
                message += "\n" + e.Message;
            };
            DataTable t1 = new DataTable();
            using (SqlDataAdapter a = new SqlDataAdapter(cmd))
            {
                a.Fill(t1);
            }
            StreamWriter sw = new StreamWriter("..\\..\\log_1.txt");
            sw.WriteLine(message);
            sw.Close();
            db.closeConnection();
            Lectura lectura = new Lectura("Relaciones que existen y deberían existir", "Lista de posibles relaciones:", t1);
            lectura.Show();
           
        }
        private void btnAnomaliasIntegridad_Click(object sender, EventArgs e)
        {
            Lectura lectura = new Lectura("Log Anomalías Integridad", null, null);
            lectura.Show();
        }

        void getAnomaliasDatos()
        {
            db.openConnection();
            
            string query = @"declare @esquemas TABLE (esquema varchar(64));
                    insert into @esquemas select distinct table_schema from INFORMATION_SCHEMA.TABLES

                    declare @esquema varchar(64)
                    declare cursor_esquemas cursor for
                    select * from @esquemas
                    open cursor_esquemas
                    fetch next from cursor_esquemas into @esquema

                    WHILE @@FETCH_STATUS = 0
                    BEGIN
	                    DECLARE @fk VARCHAR(100)
	                    DECLARE cursor_fk cursor for
	                    select fks.name from
	                    sys.foreign_key_columns fkcol
	                    join sys.foreign_keys fks on fks.object_id = fkcol.constraint_object_id
	                    join sys.tables t on t.object_id = fkcol.parent_object_id
	                    join sys.schemas s on s.schema_id = t.schema_id
	                    where s.name = @esquema
	                    open cursor_fk
	                    fetch next from cursor_fk into @fk

	                    WHILE @@FETCH_STATUS = 0
		                    BEGIN
			                    declare @dbcc table(tabla varchar(128), restriccion varchar(128), anomalia varchar(max), esquema varchar(100))
			                    insert into @dbcc(tabla,restriccion,anomalia)
			                    exec('DBCC CHECKCONSTRAINTS(''' + @fk + ''')')
			                    update @dbcc set esquema = @esquema
			                    fetch next from cursor_fk into @fk
		                    END
		                    close cursor_fk
		                    deallocate cursor_fk

	                    DECLARE @cc VARCHAR(100)
	                    DECLARE cursor_cc cursor for
	                    select cc.name from sys.check_constraints as cc
	                    join sys.schemas as s on s.schema_id = cc.schema_id
	                    where s.name = @esquema
	                    open cursor_cc
	                    fetch next from cursor_cc into @cc

	                    WHILE @@FETCH_STATUS = 0
		                    BEGIN
			                    insert into @dbcc(tabla,restriccion,anomalia)
			                    exec('DBCC CHECKCONSTRAINTS(''' + @cc + ''')')
			                    update @dbcc set esquema = @esquema
			                    fetch next from cursor_cc into @cc
		                    END
		                    close cursor_cc
		                    deallocate cursor_cc

	                    select * from @dbcc
	                    delete from @dbcc
	                    fetch next from cursor_esquemas into @esquema

                    END
                    close cursor_esquemas 
                    deallocate cursor_esquemas";
            SqlCommand cmd = new SqlCommand(query, db.getConnection());
            DataTable t1 = new DataTable();
            using (SqlDataAdapter a = new SqlDataAdapter(cmd))
            {
                a.Fill(t1);
            }
            StreamWriter sw = new StreamWriter("..\\..\\log.txt");
            foreach (DataRow dr in t1.Rows)
            {
                string line = "En la tabla " + dr["tabla"].ToString() + ";";
                line += " con la restricciín " + dr["restriccion"].ToString() + ";";
                line += " tiene la anomalía: " + dr["anomalia"].ToString() + ";";
                sw.WriteLine(line);
            }
            sw.Close();
            db.closeConnection();
            Lectura lectura = new Lectura("Log de Anomalías Detectadas", "Lista de anomalías registradas:", t1);
            lectura.Show();
        }

        private void btnAnomaliasDatos_Click(object sender, EventArgs e)
        {
            getAnomaliasDatos();
            
        }

        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            user = tbUser.Text;
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            password = tbPassword.Text;
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                testConnection();
            }
        }
    }
}
