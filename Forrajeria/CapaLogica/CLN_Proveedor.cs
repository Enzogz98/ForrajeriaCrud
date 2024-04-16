using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;

namespace CapaLogica
{
    public class CLN_Proveedor
    {
        private CAD_Proveedor objProovedoresCAD;
        private DataTable Tabla;

        public CLN_Proveedor()
        { 
            objProovedoresCAD = new CAD_Proveedor();
            Tabla = new DataTable();
        }

        public DataTable consultarProveedores()
        {
            Tabla = objProovedoresCAD.consultarProveedores();
            return Tabla;

        }
        public void AgregarProovedor(string Nombre, string direccion, string telefono)
        {
            objProovedoresCAD.agregarProveedor(Nombre, direccion, telefono);
        }
        public void ModificarProovedor(int id, string Nombre, string direccion, string telefono)
        {
            objProovedoresCAD.actualizarProveedor(id, Nombre, direccion, telefono);
        }

        public void EliminarProovedores(int id)
        {
            objProovedoresCAD.eliminarProveedor(id);
        }


    }
}
