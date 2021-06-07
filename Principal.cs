﻿using miapp_r2.entidades; // hacemos using para vincular este form con entidad usuario
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace miapp_r2
{
    public partial class Principal : Form
    {
        public Principal(Usuario usu)
        {
            InitializeComponent();

            // basicamente hacemos esto para validar el enlazado con la entidad/objeto persona
            lblAviso.Text = "Bienvenido " + usu.NombreDeUsuario;
            lblAviso.Visible = true;
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void altaPersonaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaPersona ventana = new AltaPersona();
            ventana.Show();
            
        }

        private void consultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios ventana = new Usuarios();
            ventana.Show();
        }

        private void altaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaCurso ventana = new AltaCurso();
            ventana.ShowDialog(); // ShowDialog no me permite interactuar con la ventana padre
        }
    }
}
