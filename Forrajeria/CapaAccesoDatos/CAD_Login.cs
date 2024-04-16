using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class CAD_Login
    {
        private Conexion_DB objConexionCAD;
        private SqlDataReader leerTabla ;
        private DataTable Tabla;
        private SqlCommand Comando;

        public CAD_Login()
        {
            objConexionCAD = new Conexion_DB();
            Tabla = new DataTable();
            Comando = new SqlCommand();
        }
        public DataTable Acceso(string User, string Pass)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "ingreso_PA";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            Comando.Parameters.AddWithValue("@user", User);
            Comando.Parameters.AddWithValue("@pass", Pass);
            leerTabla = Comando.ExecuteReader();
            Tabla.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return Tabla;
        }

    }
}
