using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class Conexion_DB 
    {
        private string cadena;
        private SqlConnection conectar_DB;

        public Conexion_DB()
        {
            cadena = "Data Source=DESKTOP-CDBSAN0\\SQLEXPRESS;Initial Catalog = Forrajeria; Integrated Security=true";
            conectar_DB = new SqlConnection();
            conectar_DB.ConnectionString = cadena;

        }

        public SqlConnection abrirConexion()
        {
            try
            {
                conectar_DB.Open();
                return conectar_DB;
            }
            catch (Exception)
            {
                return conectar_DB;

            }

        }

        public void cerrarConexion()
        {
            conectar_DB.Close();
        }



    }
}
