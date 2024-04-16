using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class CAD_Proveedor
    {
        private Conexion_DB objConexionCAD;
        private SqlDataReader leerTabla;
        private DataTable  miTabla;
        private SqlCommand comando;

        public CAD_Proveedor() 
        {
            objConexionCAD = new Conexion_DB();
            miTabla = new DataTable();
            comando = new SqlCommand();
        }
        public DataTable consultarProveedores()
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "sp_ListarProveedores";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            leerTabla = comando.ExecuteReader();
            miTabla.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return miTabla;
        }
        public void agregarProveedor(string nombre, string direccion, string telefono)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "sp_InsertarProveedor";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Direccion", direccion);
            comando.Parameters.AddWithValue("@Telefono", telefono);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }
        public void actualizarProveedor(int idProveedor, string nombre, string direccion, string telefono)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "sp_ActualizarProveedor";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ProveedorId", idProveedor);
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Direccion", direccion);
            comando.Parameters.AddWithValue("@Telefono", telefono);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public void eliminarProveedor(int idProveedor)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "sp_EliminarProveedor";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@ProveedorId", idProveedor);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }
    }
}
