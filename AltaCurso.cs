using miapp_r2.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace miapp_r2
{
    public partial class AltaCurso : Form
    {
        public AltaCurso()
        {
            InitializeComponent();
        }

        private void AltaCurso_Load(object sender, EventArgs e)
        {
            //txtNombre.Focus();
            CargarFecha();
            ObtenerUltimoCurso();
        }

        private void CargarFecha()
        {
            txtFecha.Text = DateTime.Now.ToShortDateString(); // ToShortDateString: ejemplo: 25/05/2021
        }

        private void ObtenerUltimoCurso()
        {
            int id = ADCursos.ObtenerUltimoIDCurso();
            txtNroCurso.Text = (id + 1).ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tablaResultado = ADCarreras.ObtenerCarrerasID(int.Parse(txtNroCarrera.Text));
                if (tablaResultado.Rows.Count > 0)
                {
                    txtCarrera.Text = tablaResultado.Rows[0][1].ToString(); // fila [0] columna [1] 
                }
                else
                {
                    MessageBox.Show("Carrera inexistente");
                    txtNroCarrera.Focus();
                    txtCarrera.Text = "";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("error al obtener carrera");
            }
        }

        private void btnBuscarAlumno_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tablaResultado = ADPersonas.ObtenerPersonaXDNI(txtDNIAlumno.Text.Trim());
                if (tablaResultado.Rows.Count > 0)
                {
                    txtNombreAlumno.Text = tablaResultado.Rows[0][1].ToString(); // fila [0] columna [1]
                    txtApellidoAlumno.Text = tablaResultado.Rows[0][2].ToString(); // fila [0] columna [2]
                    txtIDAlumno.Text = tablaResultado.Rows[0][0].ToString(); // fila [0] columna [0]
                }
                else
                {
                    MessageBox.Show("alumno no encontrado");
                    txtDNIAlumno.Focus();
                    txtDNIAlumno.Text = "";
                    txtApellidoAlumno.Text = "";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("error al obtener persona/alumno");
            }
        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            dgAlumnos.Rows.Add(txtIDAlumno.Text, txtDNIAlumno.Text, txtNombreAlumno.Text, txtApellidoAlumno.Text); // agregamos datos a la primera fila del datagrid
        }

        private void btnGuardarCurso_Click(object sender, EventArgs e)
        {
            List<int> listaAlumnos = new List<int>();
            for (int i = 0; i < dgAlumnos.Rows.Count; i++)
            {
                listaAlumnos.Add(int.Parse(dgAlumnos.Rows[i].Cells[0].Value.ToString()));
            }

            bool resultado = ADCursos.AltaNuevoCurso(int.Parse(txtNroCurso.Text), txtNombre.Text, txtDescripcion.Text.Trim(), int.Parse(txtNroCarrera.Text), listaAlumnos);

            if (resultado)
            {
                MessageBox.Show("Curso dado de alta");
            }
            else
            {
                MessageBox.Show("Error al dar alta al curso");
            }

        } // me toma todas las filas con sus repectivas columnas para agregarla a una fila auxiliar
    }
}
