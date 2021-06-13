using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace miapp_r2.AccesoADatos
{
    public class ADCursos
    {

        public static int ObtenerUltimoIDCurso()
        {
            // conexion base de datos con este form
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD2"];
            SqlConnection conexion = new SqlConnection(cadenaConexion);


            try
            {
                SqlCommand comando = new SqlCommand();

                // hacemos las consultas a la base de datos
                string consulta = "SELECT MAX(id) FROM cursos2"; // MAX(id) devuleve el valor maximo de la columna id

                comando.Parameters.Clear(); // limpiar todos los parametros que pueda tener

                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open(); // abrimos conexion a la BD
                comando.Connection = conexion;

                int resultado = (int)comando.ExecuteScalar();
                return resultado;
            }
            catch (Exception ex)
            {

                return 0;
            }
            finally
            {
                conexion.Close();
            }

        }

        public static bool AltaNuevoCurso(int idCurso, string nombreCurso, string descripcionCurso, int idCarrera, List<int> listaIdAlumnos)
        {
            // conexion base de datos con este form
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD2"];
            SqlTransaction objTransaccion = null;
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand comando = new SqlCommand();

                // hacemos las consultas a la base de datos
                string consulta = "insert into cursos2 values(@Nombre,@Descripcion,@IdCarrera)";

                comando.Parameters.Clear(); // limpiar todos los parametros que pueda tener
                comando.Parameters.AddWithValue("@Nombre", nombreCurso);
                comando.Parameters.AddWithValue("@Descripcion", descripcionCurso);
                comando.Parameters.AddWithValue("@IdCarrera", idCarrera);
                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open(); // abrimos conexion a la BD
                objTransaccion = conexion.BeginTransaction("AltaDeCurso");
                comando.Transaction = objTransaccion;
                comando.Connection = conexion;

                comando.ExecuteNonQuery();

                foreach (var idAlumno in listaIdAlumnos)
                {
                    string consultaCursoPorAlumnno = "insert into personas_por_cursos values(@IdPersona,@IdCurso,@fecha)";
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@IdPersona", idAlumno);
                    comando.Parameters.AddWithValue("@IdCurso", idCurso);
                    comando.Parameters.AddWithValue("@fecha", DateTime.Now);

                    comando.CommandText = consultaCursoPorAlumnno;
                    comando.ExecuteNonQuery();
                }

                objTransaccion.Commit();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                objTransaccion.Rollback();
                return false;
            }
            finally
            {
                conexion.Close();
            }

        } // transaccion

        public static DataTable ObtenerListadoDeCursos()
        {

            // conexion base de datos con este form
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD2"];
            SqlCommand comando = new SqlCommand();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            // hacemos las consultas a la base de datos
            string consulta = "SELECT * FROM cursos2";

            comando.Parameters.Clear(); // limpiar todos los parametros que pueda tener

            comando.CommandType = CommandType.Text;
            comando.CommandText = consulta;

            conexion.Open(); // abrimos conexion a la BD
            comando.Connection = conexion;

            DataTable tabla = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(tabla);

            return tabla;

        } // [Form ReporteListadoCursos] conexion a la BD para traer los datos a la grilla

        public static DataTable ObtenerEstadisticasCursos()
        {

            // conexion base de datos con este form
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD2"];
            SqlCommand comando = new SqlCommand();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            // hacemos las consultas a la base de datos
            string consulta = "Select c.Nombre, COUNT(pc.IdPersona) as Cantidad FROM personas_por_cursos pc Inner join cursos2 c On pc.IdCurso = c.Id Group by c.Nombre; ";

            comando.Parameters.Clear(); // limpiar todos los parametros que pueda tener

            comando.CommandType = CommandType.Text;
            comando.CommandText = consulta;

            conexion.Open(); // abrimos conexion a la BD
            comando.Connection = conexion;

            DataTable tabla = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(tabla);

            return tabla;

        } // [Form ReporteListadoCursos] conexion a la BD para traer los datos a la grilla


    }


}
