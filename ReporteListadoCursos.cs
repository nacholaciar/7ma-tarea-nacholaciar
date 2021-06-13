using miapp_r2.AccesoADatos;
using Microsoft.Reporting.WinForms;
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
    public partial class ReporteListadoCursos : Form
    {
        public ReporteListadoCursos()
        {
            InitializeComponent();
        }

        private void ReporteListadoCursos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla '_miapp_r4DataSet.cursos2' Puede moverla o quitarla según sea necesario.
            this.cursos2TableAdapter.Fill(this._miapp_r4DataSet.cursos2);

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            tabla = ADCursos.ObtenerListadoDeCursos();

            ReportDataSource ds = new ReportDataSource("DatosCursos", tabla);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(ds);
            reportViewer1.LocalReport.Refresh();
        }   
    }
}
