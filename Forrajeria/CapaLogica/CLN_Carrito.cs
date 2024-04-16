using CapaAccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLN_Carrito
    {
        private CAD_Carrito objCarritoCAD;
        private DataTable Tabla;
        private DataTable Tabla2;
        private DataTable Tabla3;
        private DataTable Tabla4;
        private DataTable Tabla5;
        private DataTable Tabla6;
        private DataTable Carrito;
        private decimal total;
        int codigo;
        string nombre;
        decimal precio;
        decimal Total;
        int stock;
        int IdCliente;
        int IDVenta;


        public CLN_Carrito()
        {
            objCarritoCAD = new CAD_Carrito();
            Tabla = new DataTable();
            Tabla2 = new DataTable();
            Tabla3 = new DataTable();
            Tabla4 = new DataTable();
            Tabla5 = new DataTable();
            Tabla6 = new DataTable();
            Carrito = new DataTable();
            Carrito.Columns.Add("Codigo",typeof(int));
            Carrito.Columns.Add("Nombre", typeof(string));
            Carrito.Columns.Add("Precio", typeof(decimal));
            Carrito.Columns.Add("Cantidad", typeof(int));
            Carrito.Columns.Add("Total", typeof(decimal));
            total = 0;
            stock = 0;
            
        }

        public DataTable ProductosDisponibles()
        {
            Tabla = objCarritoCAD.ProductosDisponibles();
            return Tabla;

        }
        public DataTable FiltrarText(string Texto)

        {
            Tabla2.Clear();
            Tabla2 = objCarritoCAD.FiltrarText(Texto);
            return Tabla2;

        }
        public DataTable FiltrarId(int IdProd)

        {
            Tabla2.Clear();
            Tabla2 = objCarritoCAD.FiltrarID(IdProd);
            return Tabla2;

        }

        public decimal PrecioXcantidad(int Cantidad, decimal Precio)
        {
            total = Cantidad * Precio;
            return total; 
        }

        public DataTable DetalleCarrito(int IdProd,int Cantidad)
        {
            
            Tabla3.Clear();
            Tabla3 = objCarritoCAD.ProdDetalle(IdProd);
            foreach (DataRow row in Tabla3.Rows)
            {
                codigo = IdProd ;
                nombre = row["Nombre"].ToString() ;
                precio = (decimal)row["Precio"];
                Total = precio * Cantidad;
               
            }
            Carrito.Rows.Add(codigo,nombre, precio, Cantidad, Total);
            return Carrito;
        }
        public void RestarStock(int IdProd, int Stock)
        {
            objCarritoCAD.RestarStock(IdProd, Stock);
        }
        public int StockActual(int IdProd)
        {
            Tabla4.Clear();
            Tabla4 = objCarritoCAD.stock(IdProd);
            foreach (DataRow row in Tabla4.Rows)
            {
                stock = (int)row["Stock"];
            }    
            return stock;
        }

        public void RestaurarStock(int IdProd, int Stock)
        {
            objCarritoCAD.RestaurarStock(IdProd, Stock);
        }
        public DataTable EditarCarrito(int Indice,decimal Precio ,int Cantidad)
        {
            Carrito.Rows[Indice]["Cantidad"] = Cantidad;
            Carrito.Rows[Indice]["Total"] = Precio * Cantidad;
            return Carrito;
        }
        public DataTable EliminarItemCarro(int Indice)
        {
            int Cantidad = Convert.ToInt32(Carrito.Rows[Indice]["Cantidad"]);
            int idProd = (int)Carrito.Rows[Indice]["Codigo"];
            RestaurarStock(idProd, Cantidad);
            Carrito.Rows.RemoveAt(Indice);
            return Carrito;
        }
         
        public int InsertCliente(string Nombre, string Direccion, string Telefono)
        {
            IdCliente= objCarritoCAD.insertCliente(Nombre, Direccion, Telefono);
            return IdCliente;
        }
        public int insertVenta(string Fecha, int ClienteID, decimal total)
        {
           
            IDVenta = objCarritoCAD.insertVenta(Fecha, ClienteID, total);
            return IDVenta;
        }
        public void insertDetallesVenta(int VentaID, int ProductoID, int Cantidad, decimal PrecioUnitario)
        {
            objCarritoCAD.insertDetallesVenta(VentaID,ProductoID,Cantidad,PrecioUnitario);
        }

       public int ObtenerNumeroFactura()
        {
            int idVenta;
            idVenta = objCarritoCAD.ObtenerNroFactura();
            return idVenta;
               
        }



    }
}
