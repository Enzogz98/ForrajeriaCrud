using CapaAccesoDatos;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLN_Estadistica
    {

        private CAD_Estadistica objCAD_Estadistica;
        private DataTable Grafico;
         
        public CLN_Estadistica()
        {
            objCAD_Estadistica = new CAD_Estadistica();
            Grafico = new DataTable();
        }
        public DataTable ProdCantidad()
        {
            Grafico.Clear();
            Grafico = objCAD_Estadistica.ProdCantidad();
            return Grafico;

        }
    }
}
