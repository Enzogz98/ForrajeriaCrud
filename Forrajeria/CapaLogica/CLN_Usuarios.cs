using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;

namespace CapaLogica
{
    public class CLN_Usuarios
    {
        private CAD_Usuarios objUsuariosCAD;
        private DataTable Tabla;

        public CLN_Usuarios()
        {
            objUsuariosCAD = new CAD_Usuarios();
            Tabla = new DataTable();
        }

        public DataTable ObtenerUsuarios()
        {
            Tabla = objUsuariosCAD.MostrarUsuarios();
            return Tabla;
        }

        public void CrearUsuario(string nombreUsuario, string pass, string nombreCompleto, string rol)
        {
            objUsuariosCAD.CrearUsuario(nombreUsuario, pass, nombreCompleto, rol);
        }

        public void ActualizarUsuario(int usuarioID, string nombreUsuario, string pass, string nombreCompleto, string rol)
        {
            objUsuariosCAD.ActualizarUsuario(usuarioID, nombreUsuario, pass, nombreCompleto, rol);
        }

        public void EliminarUsuario(int usuarioID)
        {
            objUsuariosCAD.EliminarUsuario(usuarioID);
        }
    }
}
