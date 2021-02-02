using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Funciones;

namespace WindowsFormsApp1
{
    public partial class Main : Form
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

            Lectura lectura = new Lectura("Log Relaciones");
            lectura.Show();

        }

        private void btnAnomaliasIntegridad_Click(object sender, EventArgs e)
        {
            Lectura lectura = new Lectura("Log Anonalias Integridad");
            lectura.Show();
        }

        private void btnAnomaliasDatos_Click(object sender, EventArgs e)
        {
            Lectura lectura = new Lectura("Log Anomalias Datos");
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
