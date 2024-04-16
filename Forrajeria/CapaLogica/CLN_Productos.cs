using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;

namespace CapaLogica
{
    public class CLN_Productos
    {
        private CAD_Productos objProductosCAD;
        private DataTable Tabla;
        private DataTable Tabla2;
        private DataTable Combo1;

        public CLN_Productos()
        {
            objProductosCAD = new CAD_Productos();
            Tabla = new DataTable();
            Tabla2 = new DataTable();
            Combo1 = new DataTable();
        }

        public DataTable MostrarProductos()
        {
            Tabla = objProductosCAD.MostrarProductos();
            return Tabla;

        }


        public void AgregarProductos(string Nom, decimal Precio, string Descripcion, int Stock, int IdProv)
        { 
            objProductosCAD.AgregarProducto(Nom,Precio,Descripcion,Stock,IdProv);
        }

        public void EditarProductos(int IdProd, string Nom, decimal Precio, string Descripcion, int Stock, int IdProv)
        {
            objProductosCAD.EditarProductos(IdProd, Nom, Precio,Descripcion, Stock, IdProv);
        }
        public void EliminarProductos(int IdProd)

        {
            objProductosCAD.EliminarProductos(IdProd);
        }

        public DataTable FiltrarText(string Texto)

        {
            Tabla2.Clear();
            Tabla2 = objProductosCAD.FiltrarText(Texto);
            return Tabla2;
            
        }
        public DataTable FiltrarId(int IdProd)

        {
            Tabla2.Clear();
            Tabla2 = objProductosCAD.FiltrarID(IdProd);
            return Tabla2;

        }
        public DataTable ComboProveedor()

        {
            Combo1.Clear();
            Combo1 = objProductosCAD.ComboProveedores() ;
            return Combo1;

        }

        public void insertarCompra(string fecha, int idProv, decimal importe)
        {
            objProductosCAD.insertCompra(fecha, idProv, importe);
        }
    }
}
