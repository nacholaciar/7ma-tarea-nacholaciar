﻿using System;
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
    public partial class ReporteListadoCursos : Form
    {
        public ReporteListadoCursos()
        {
            InitializeComponent();
        }

        private void ReporteListadoCursos_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }   
    }
}
