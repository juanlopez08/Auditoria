using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Lectura : Form
    {
        public Lectura(String titulo, String tituloTabla, DataTable t1, int opType)
        {
            InitializeComponent();
            txtNombreLog.Text = titulo;
            txtDescTabla.Text = tituloTabla;
            dataGridView1.DataSource = t1;
            dataGridView1.AllowUserToAddRows = false;
            WindowState = FormWindowState.Maximized;

            // TXT UBICADO EN LA MISMA CARPETA DEL PROYECTO
            switch (opType)
            {
                case 0:
                    Console.WriteLine("Relaciones");
                    string[] lines = System.IO.File.ReadAllLines(@"..\\..\\log_1.txt");
                    string allLines = String.Join("\r\n", lines);
                    txtLog.Text = allLines;
                    break;
                case 1:
                    Console.WriteLine("Anomalías de Integridad Referencial");
                    Console.WriteLine("Relaciones");
                    string[] linest = System.IO.File.ReadAllLines(@"..\\..\\log_2.txt");
                    string allLinest = String.Join("\r\n", linest);
                    txtLog.Text = allLinest;
                    break;
                case 2:
                    Console.WriteLine("Anomalías de Datos");
                    txtLog.Text = System.IO.File.ReadAllText("..\\..\\log.txt"); //Anomalías con datos
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
           

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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.DeselectAll();
        }
    }

}
