using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Lectura : Form
    {
        public Lectura(String titulo, String tituloTabla, DataTable t1)
        {
            InitializeComponent();
            txtNombreLog.Text = titulo;
            txtDescTabla.Text = tituloTabla;
            dataGridView1.DataSource = t1;


            // TXT UBICADO EN LA MISMA CARPETA DEL PROYECTO
            
            /*Anomalías con Datos*/
            //txtLog.Text = System.IO.File.ReadAllText("..\\..\\log.txt"); //Anomalías con datos
            /**********************/

            /*Relaciones que deberían existir*/
            string[] lines = System.IO.File.ReadAllLines(@"..\\..\\log_1.txt");
            string allLines = String.Join("\r\n", lines);
            txtLog.Text = allLines;
            /*********************************/

            // ABRIR CON EXPLORADOR DE ARCHIVOS

            /*System.Windows.Forms.OpenFileDialog dSelFichero =
                   new System.Windows.Forms.OpenFileDialog();
            dSelFichero.Filter = "Ficheros de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            dSelFichero.Title = "Fichero de texto";
            dSelFichero.DefaultExt = "txt";
            if (dSelFichero.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (System.IO.File.Exists(dSelFichero.FileName))
                {
                    txtLog.Text = System.IO.File.ReadAllText(dSelFichero.FileName);
                }
            }*/
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
