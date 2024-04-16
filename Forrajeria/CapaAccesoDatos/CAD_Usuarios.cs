using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class CAD_Usuarios
    {
        private Conexion_DB objConexionCAD;
        private SqlCommand Comando;
        private SqlDataReader leerTabla;
        private DataTable Tabla;

        public CAD_Usuarios()
        {
            objConexionCAD = new Conexion_DB();
            Comando = new SqlCommand();
            Tabla = new DataTable();
        }

        public DataTable MostrarUsuarios()
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "ObtenerUsuarios";
            Comando.CommandType = CommandType.StoredProcedure;
            leerTabla = Comando.ExecuteReader();
            Tabla.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return Tabla;
        }

        public void CrearUsuario(string nombreUsuario, string pass, string nombreCompleto, string rol)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "CrearUsuario";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
            Comando.Parameters.AddWithValue("@Pass", pass);
            Comando.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
            Comando.Parameters.AddWithValue("@Rol", rol);
            Comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public void ActualizarUsuario(int usuarioID, string nombreUsuario, string pass, string nombreCompleto, string rol)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "ActualizarUsuario";
            Comando.CommandType =  CommandType.StoredProcedure;
            Comando.Parameters.AddWithValue("@UsuarioID", usuarioID);
            Comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
            Comando.Parameters.AddWithValue("@Pass", pass);
            Comando.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
            Comando.Parameters.AddWithValue("@Rol", rol);
            Comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public void EliminarUsuario(int usuarioID)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "EliminarUsuario";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.AddWithValue("@UsuarioID", usuarioID);
            Comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }
    }
}
