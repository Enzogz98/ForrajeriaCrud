using CapaAccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaLogica
{
    public class CLN_Clientes
    {
        private CAD_Clientes objClientesCAD;
        private DataTable Tabla;

        public CLN_Clientes()  
        {
            objClientesCAD = new CAD_Clientes();
            Tabla = new DataTable();
        }

        public DataTable ObtenerClientes()
        {
            Tabla = objClientesCAD.ObtenerClientes();
            return Tabla;

        }
        public void InsertarCliente(string Nombre, string direccion, string telefono)
        {
            objClientesCAD.InsertarCliente(Nombre, direccion, telefono);
        }
        public void ActualizarClienter(int id, string Nombre, string direccion, string telefono)
        {
            objClientesCAD.ActualizarCliente(id, Nombre, direccion, telefono);
        }

        public void EliminarCliente(int id)
        {
            objClientesCAD.EliminarCliente(id);
        }
    }
}
