
namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbNombre = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnProbarConexion = new System.Windows.Forms.Button();
            this.btnRelaciones = new System.Windows.Forms.Button();
            this.btnAnomaliasIntegridad = new System.Windows.Forms.Button();
            this.btnAnomaliasDatos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Auditoría de Base de datos";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbNombre
            // 
            this.tbNombre.Location = new System.Drawing.Point(83, 39);
            this.tbNombre.Name = "tbNombre";
            this.tbNombre.Size = new System.Drawing.Size(217, 20);
            this.tbNombre.TabIndex = 1;
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(83, 65);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(217, 20);
            this.tbUser.TabIndex = 2;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(83, 91);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(217, 20);
            this.tbPassword.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Usuario:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nombre DB:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnProbarConexion
            // 
            this.btnProbarConexion.Location = new System.Drawing.Point(106, 130);
            this.btnProbarConexion.Name = "btnProbarConexion";
            this.btnProbarConexion.Size = new System.Drawing.Size(155, 31);
            this.btnProbarConexion.TabIndex = 7;
            this.btnProbarConexion.Text = "Probar Conexión";
            this.btnProbarConexion.UseVisualStyleBackColor = true;
            this.btnProbarConexion.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRelaciones
            // 
            this.btnRelaciones.Location = new System.Drawing.Point(12, 179);
            this.btnRelaciones.Name = "btnRelaciones";
            this.btnRelaciones.Size = new System.Drawing.Size(113, 42);
            this.btnRelaciones.TabIndex = 8;
            this.btnRelaciones.Text = "Relaciones de integridad referencial";
            this.btnRelaciones.UseVisualStyleBackColor = true;
            // 
            // btnAnomaliasIntegridad
            // 
            this.btnAnomaliasIntegridad.Location = new System.Drawing.Point(137, 179);
            this.btnAnomaliasIntegridad.Name = "btnAnomaliasIntegridad";
            this.btnAnomaliasIntegridad.Size = new System.Drawing.Size(115, 42);
            this.btnAnomaliasIntegridad.TabIndex = 9;
            this.btnAnomaliasIntegridad.Text = "Anomalías sobre integridad referencial";
            this.btnAnomaliasIntegridad.UseVisualStyleBackColor = true;
            // 
            // btnAnomaliasDatos
            // 
            this.btnAnomaliasDatos.Location = new System.Drawing.Point(259, 179);
            this.btnAnomaliasDatos.Name = "btnAnomaliasDatos";
            this.btnAnomaliasDatos.Size = new System.Drawing.Size(97, 42);
            this.btnAnomaliasDatos.TabIndex = 10;
            this.btnAnomaliasDatos.Text = "Anomalías de datos";
            this.btnAnomaliasDatos.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 236);
            this.Controls.Add(this.btnAnomaliasDatos);
            this.Controls.Add(this.btnAnomaliasIntegridad);
            this.Controls.Add(this.btnRelaciones);
            this.Controls.Add(this.btnProbarConexion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.tbNombre);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Auditoría de BD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNombre;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnProbarConexion;
        private System.Windows.Forms.Button btnRelaciones;
        private System.Windows.Forms.Button btnAnomaliasIntegridad;
        private System.Windows.Forms.Button btnAnomaliasDatos;
    }
}

