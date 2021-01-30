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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // VARIABLES GLOBALES
        string nombreBD;
        string user;
        string password;


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
            
        }

        private void tbNombre_TextChanged(object sender, EventArgs e)
        {
            nombreBD = tbNombre.Text;
        }

        private void btnRelaciones_Click(object sender, EventArgs e)
        {
            MessageBox.Show("BD> " + nombreBD + "\nuser> " + user + "\npass> " + password);
        }

        private void btnAnomaliasIntegridad_Click(object sender, EventArgs e)
        {

        }

        private void btnAnomaliasDatos_Click(object sender, EventArgs e)
        {

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
