using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class CAD_Clientes 
    {
        private Conexion_DB objConexionCAD;
        private SqlDataReader leerTabla;
        private DataTable miTabla;
        private SqlCommand comando;
        public CAD_Clientes()
        {
            objConexionCAD = new Conexion_DB();
            miTabla = new DataTable();
            comando = new SqlCommand();
        }
        public DataTable ObtenerClientes()
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "ObtenerClientes";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            leerTabla = comando.ExecuteReader();
            miTabla.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return miTabla;
        }
        public void InsertarCliente(string nombre, string direccion, string telefono)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "InsertarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Direccion", direccion);
            comando.Parameters.AddWithValue("@Telefono", telefono);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }
        public void ActualizarCliente(int idCliente, string nombre, string direccion, string telefono)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "ActualizarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ClienteID", idCliente);
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Direccion", direccion);
            comando.Parameters.AddWithValue("@Telefono", telefono);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public void EliminarCliente(int idCliente)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "EliminarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ClienteID", idCliente);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }
    }
    
}
