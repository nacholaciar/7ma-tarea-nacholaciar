using System;
using System.Data;
using System.Data.SqlClient;

namespace miapp_r2.AccesoADatos
{
    public class ADCarreras
    {

        public static DataTable ObtenerCarrerasID(int idCarrera)
        {
            // conexion base de datos con este form
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD2"];
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            

            try
            {
                SqlCommand comando = new SqlCommand();

                // hacemos las consultas a la base de datos
                string consulta = "SELECT * from TipoCarreraBD where Id = @id"; 

                comando.Parameters.Clear(); // limpiar todos los parametros que pueda tener
                comando.Parameters.AddWithValue("@id", idCarrera);
                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta;

                conexion.Open(); // abrimos conexion a la BD
                comando.Connection = conexion;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conexion.Close();
            }

        }
    }


}
