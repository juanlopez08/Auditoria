﻿using System;
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
        public Lectura(String titulo)
        {
            InitializeComponent();
            txtNombreLog.Text = titulo;

            // TXT UBICADO EN LA MISMA CARPETA DEL PROYECTO
            txtLog.Text = System.IO.File.ReadAllText("..\\..\\log.txt");
            
            // ABRIR CON EXPLORADOR DE ARCHIVOS

            //System.Windows.Forms.OpenFileDialog dSelFichero =
            //       new System.Windows.Forms.OpenFileDialog();
            //dSelFichero.Filter = "Ficheros de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            //dSelFichero.Title = "Fichero de texto";
            //dSelFichero.DefaultExt = "txt";
            //if (dSelFichero.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    if (System.IO.File.Exists(dSelFichero.FileName))
            //    {
            //        txtLog.Text = System.IO.File.ReadAllText(dSelFichero.FileName);
            //    }
            //}
        }
    }

}