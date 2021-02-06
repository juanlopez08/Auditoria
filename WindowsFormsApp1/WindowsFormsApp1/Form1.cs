using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string[] args = { tbNombre.Text, tbUser.Text, tbPassword.Text };
            connected = db.connect(args);
            if (connected)
            {
                MessageBox.Show("Conexión establecida exitósamente!");
                btnAnomaliasDatos.Enabled = true;
                btnRelaciones.Enabled = true;
                btnAnomaliasIntegridad.Enabled = true;
            }
            else
                MessageBox.Show("Conexión fallida, verifique los datos ingresados.");

            // SOLO ESTAN PARA PROBAR LOS BOTONES
            btnAnomaliasDatos.Enabled = true;
            btnRelaciones.Enabled = true;
            btnAnomaliasIntegridad.Enabled = true;

        }

        private void tbNombre_TextChanged(object sender, EventArgs e)
        {
            nombreBD = tbNombre.Text;
        }

        private void btnRelaciones_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("BD> " + nombreBD + "\nuser> " + user + "\npass> " + password);

            Lectura lectura = new Lectura("Log Relaciones", null);
            lectura.Show();

        }

        private void btnAnomaliasIntegridad_Click(object sender, EventArgs e)
        {
            Lectura lectura = new Lectura("Log Anonalias Integridad", null);
            lectura.Show();
        }

        DataTable dtRelationships = new DataTable();
        private void btnAnomaliasDatos_Click(object sender, EventArgs e)
        {
            db.openConnection();

            //SqlConnection conn = db.getConnection();                
            //new SqlConnection(connectionStringGlobal);
            //conn.Open();
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
            dtRelationships = t1;

            StreamWriter sw = new StreamWriter("..\\..\\log.txt"); //create the file
            foreach (DataRow dr in t1.Rows)
            {
                string line = "En la tabla " + dr["tabla"].ToString() + ";";
                line += " con la restriccion " + dr["restriccion"].ToString() + ";";
                line += " tiene la anomalia: " + dr["anomalia"].ToString() + ";";
                //and so on
                sw.WriteLine(line); //write data
                
            }
            sw.Close();

            db.closeConnection();

            Lectura lectura = new Lectura("Log Anomalias Datos", t1);
            lectura.Show();
            
        }

            

        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            user = tbUser.Text;
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            password = tbPassword.Text;
        }
    }
}
