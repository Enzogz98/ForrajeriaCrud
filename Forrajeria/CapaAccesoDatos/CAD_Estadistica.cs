using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class CAD_Estadistica 
    {
        private Conexion_DB objConexionCAD;
        private SqlCommand Comando;
        private SqlDataReader leerTabla;
        private DataTable Grafico;

        public CAD_Estadistica()
        {
            objConexionCAD = new Conexion_DB();
            Comando = new SqlCommand();
            Grafico = new DataTable();

        }

        public DataTable ProdCantidad()
        {
            Comando.Connection = objConexionCAD.abrirConexion();
            Comando.CommandText = "ProductoCantidadesVendidas";
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            leerTabla = Comando.ExecuteReader();
            Grafico.Clear();
            Grafico.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return Grafico;
        }
    }
}
