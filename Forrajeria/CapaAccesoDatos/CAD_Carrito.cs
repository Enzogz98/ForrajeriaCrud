using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CapaAccesoDatos
{
    public class CAD_Carrito 
    {

        private Conexion_DB objConexionCAD;
        private SqlDataReader leerTabla;
        private SqlDataReader leerTabla2;
        private SqlDataReader leerTabla3;
        private SqlDataReader leerTabla4;
        private SqlDataReader leerTabla5;
        private SqlDataReader leerTabla6;
        private DataTable Tabla;
        private DataTable Tabla2;
        private DataTable Tabla3;
        private DataTable Tabla4;
        private DataTable Tabla5;
        private DataTable Tabla6;
        private DataTable Tabla7;
        private SqlCommand Comando;
        int ClienteID;
        int VentaID;
        int ultimoId;

        public CAD_Carrito()
        {
            objConexionCAD = new Conexion_DB();
            Tabla = new DataTable();
            Tabla2 = new DataTable();
            Tabla3 = new DataTable();
            Tabla4 = new DataTable();
            Tabla5 = new DataTable();
            Tabla6 = new DataTable();
            Comando = new SqlCommand();
           
           
        }

        public DataTable ProductosDisponibles()
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "ProductosDisponibles";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            leerTabla = Comando.ExecuteReader();
            Tabla.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return Tabla;
        }
        public DataTable FiltrarText(string Texto)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "FiltroProdDispText";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            Comando.Parameters.AddWithValue("@texto", Texto);
            leerTabla = Comando.ExecuteReader();
            Tabla2.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return Tabla2;

        }
        public DataTable FiltrarID(int IdProd)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "FiltroProdDispInt";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            Comando.Parameters.AddWithValue("@idProducto", IdProd);
            leerTabla = Comando.ExecuteReader();
            Tabla2.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return Tabla2;

        }

        public DataTable ProdDetalle(int IdProd) 
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "productosDetalle";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            Comando.Parameters.AddWithValue("@idProd", IdProd);
            leerTabla2= Comando.ExecuteReader(); 
            Tabla3.Load(leerTabla2);
            objConexionCAD.cerrarConexion();
            return Tabla3;
        }
        public void RestarStock (int IdProd,int Stock)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "restarStock";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            Comando.Parameters.AddWithValue("@idProd", IdProd);           
            Comando.Parameters.AddWithValue("@stock", Stock);
            Comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }
        public void RestaurarStock(int IdProd, int Stock)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "restaurarStock";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            Comando.Parameters.AddWithValue("@idProd", IdProd);
            Comando.Parameters.AddWithValue("@stock", Stock);
            Comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }
        public DataTable stock(int IdProd)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "stockActual";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            Comando.Parameters.AddWithValue("@idProd", IdProd);
            leerTabla3 = Comando.ExecuteReader();
            Tabla4.Load(leerTabla3);
            objConexionCAD.cerrarConexion();
            return Tabla4;
        }

        public int insertCliente(string Nombre,string Direccion, string Telefono)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "InsertarClienteYObtenerID";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            Comando.Parameters.AddWithValue("@nombre", Nombre);
            Comando.Parameters.AddWithValue("@direccion", Direccion);
            Comando.Parameters.AddWithValue("@telefono", Telefono);
            leerTabla4 = Comando.ExecuteReader();
            Tabla5.Load(leerTabla4);
            objConexionCAD.cerrarConexion();
            
            foreach (DataRow row in Tabla5.Rows)
            {
                ClienteID= Convert.ToInt32(row["ClienteID"]);
            }
            return ClienteID;
        }
        public int insertVenta(string Fecha, int ClienteID, decimal total)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "InsertarVentaYObtenerID";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            Comando.Parameters.AddWithValue("@fecha", Fecha);
            Comando.Parameters.AddWithValue("@ClienteID", ClienteID);
            Comando.Parameters.AddWithValue("@ImporteTotal", total);
            leerTabla5 = Comando.ExecuteReader();
            Tabla6.Load(leerTabla5);
            objConexionCAD.cerrarConexion();
            foreach (DataRow row in Tabla6.Rows)
            {
                VentaID = Convert.ToInt32(row["VentaID"]);
            }
            return VentaID;
        }
        public void insertDetallesVenta(int VentaID, int ProductoID, int Cantidad,decimal PrecioUnitario)
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "InsertarDetallesVenta";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            Comando.Parameters.AddWithValue("@VentaID", VentaID);
            Comando.Parameters.AddWithValue("@ProductoID", ProductoID);
            Comando.Parameters.AddWithValue("@Cantidad", Cantidad);
            Comando.Parameters.AddWithValue("@PrecioUnitario", PrecioUnitario);
            Comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }


        public int ObtenerNroFactura()
        {
            int ultimoID = 0;
            using (SqlConnection connection = objConexionCAD.abrirConexion())
            {
                using (SqlCommand command = new SqlCommand("ObtenerUltimoIDVenta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ultimoID = Convert.ToInt32(reader["UltimoIDVenta"]); 
                        }
                    }
                }
            }

            return ultimoID;
        }



    }
}
