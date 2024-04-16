using CapaLogica;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Forrajeria
{
    public partial class EstadisticaControl : Form
    {
        private ArrayList NOMBRES;
        private ArrayList TOTALES;
        private ArrayList CANTIDADES;
        private DataTable Grafico;
        private decimal TotalVendido;
        private CLN_Estadistica objEstadisticaCLN;
        public EstadisticaControl()
        {
            InitializeComponent();
            NOMBRES = new ArrayList();
            TOTALES = new ArrayList();
            CANTIDADES = new ArrayList();    
            Grafico = new DataTable();
            TotalVendido = 0;
            objEstadisticaCLN = new CLN_Estadistica();
            ProductosCantidad();

            this.BackColor = Color.FromArgb(29, 61, 78);

            chartGraficoBarras.BackColor = Color.Transparent;
            chartGraficoCircular.BackColor = Color.Transparent;
            chartGraficoBarras.ChartAreas[0].BackColor = Color.Transparent;
            chartGraficoCircular.ChartAreas[0].BackColor = Color.Transparent;
            chartGraficoBarras.Series[0].LabelForeColor = Color.White; // Por ejemplo, texto blanco
            chartGraficoCircular.Series[0].LabelForeColor = Color.White;
            chartGraficoBarras.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White; // Color de las etiquetas del eje X
            chartGraficoBarras.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;

            chartGraficoBarras.Series[0].Color = Color.Green;
           
        }


        private void ProductosCantidad() // productos mas vendidos por cantidad
        {
            Grafico.Clear();
            Grafico = objEstadisticaCLN.ProdCantidad();
            NOMBRES.Clear();

            TOTALES.Clear();
            foreach (DataRow row in Grafico.Rows)
            {
                NOMBRES.Add(row["Nombre"].ToString());
                CANTIDADES.Add(Convert.ToInt32(row["cantidades"]));
                TOTALES.Add(Convert.ToDecimal(row["Total Generado"]));
                TotalVendido += Convert.ToDecimal(row["Total Generado"]);

            }

            // Combina los datos en una lista de tuplas y ordénalos por Totales
            var data = Enumerable.Range(0, NOMBRES.Count)
                .Select(i => new Tuple<string, decimal>(NOMBRES[i].ToString(), Convert.ToDecimal(TOTALES[i])))
                .OrderByDescending(t => t.Item2)
                .ToList();

            // Limpia cualquier serie existente en el gráfico
            chartGraficoBarras.Series.Clear();

            // Crea una nueva serie para el gráfico de columnas
            Series series = new Series("Productos");
            series.ChartType = SeriesChartType.Column;

            series.IsValueShownAsLabel = true;

            // Agrega los datos ordenados a la serie
            foreach (var item in data)
            {
                var formattedValue = "$ " + item.Item2.ToString("N0"); // Formatea el valor como moneda
                series.Points.AddXY(item.Item1, item.Item2);
                series.Points.Last().Label = formattedValue; // Asigna la etiqueta formateada a la barra
            }

            // Agrega la serie al gráfico
            chartGraficoBarras.Series.Add(series);

            chartGraficoBarras.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartGraficoBarras.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartGraficoCircular.Series[0].Points.DataBindXY(NOMBRES, CANTIDADES);
           // chartGraficoBarras.Series[0].Points.DataBindXY(NOMBRES,TOTALES);
            chartGraficoBarras.Series[0].Name = "Total Vendido : $" + TotalVendido.ToString();
            
        }

        private void lblProductos_Click(object sender, EventArgs e)
        {

        }
    }
}
