using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;
using System.Windows.Media;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;
using System.Globalization;

namespace Forrajeria
{
    public partial class FacturaForm : Form
    {
        private int numeroFactura;
        private string cliente;
        private string direccionCliente;
        private string telefonoCliente;
        private DataTable detallesFactura;
        private decimal importeTotal;

        public FacturaForm(int numeroFactura, string cliente, string direccionCliente, string telefonoCliente, DataTable detallesFactura, decimal importeTotal)
        {
            this.numeroFactura = numeroFactura;
            this.cliente = cliente;
            this.direccionCliente = direccionCliente;
            this.telefonoCliente = telefonoCliente;
            this.detallesFactura = detallesFactura;
            this.importeTotal = importeTotal;

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Configura el tamaño del formulario y otros detalles visuales
            this.Size = new Size(600, 400);
            this.Text = "Factura";

            // Crea un botón para imprimir la factura
            Button imprimirButton = new Button();
            imprimirButton.Text = "Imprimir";
            imprimirButton.Location = new Point(10, 10);
            imprimirButton.Click += ImprimirButton_Click;
            this.Controls.Add(imprimirButton);

            // Agrega los detalles de la factura en un DataGridView
            DataGridView dgvDetallesFactura = new DataGridView();
            dgvDetallesFactura.DataSource = detallesFactura;
            dgvDetallesFactura.Location = new Point(10, 50);
            dgvDetallesFactura.Size = new Size(560, 250);
            this.Controls.Add(dgvDetallesFactura);
        }


        private void ImprimirButton_Click(object sender, EventArgs e)
        {
            string pdfFileName = "factura.pdf"; // Nombre del archivo PDF

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Factura";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 12);

            XTextFormatter tf = new XTextFormatter(gfx);
            int yPosition = 50;

        
            tf.DrawString($"Factura #{numeroFactura}", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
            yPosition += 20;
            tf.DrawString($"Cliente: {cliente}", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
            yPosition += 20;
            tf.DrawString($"Dirección: {direccionCliente}", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
            yPosition += 20;
            tf.DrawString($"Teléfono: {telefonoCliente}", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);

          
            yPosition += 20;
            foreach (DataRow row in detallesFactura.Rows)
            {
                string producto = row["Producto"].ToString();
                string cantidad = row["Cantidad"].ToString();
                string precioUnitario = row["Precio unitario"].ToString();
                string subtotal = row["Subtotal"].ToString();

                tf.DrawString(producto, font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
                yPosition += 20;
                tf.DrawString(cantidad, font, XBrushes.Black, new XRect(250, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
                tf.DrawString(precioUnitario, font, XBrushes.Black, new XRect(350, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
                tf.DrawString(subtotal, font, XBrushes.Black, new XRect(450, yPosition, page.Width, page.Height), XStringFormats.TopLeft);

                yPosition += 20;
            }

            yPosition += 20;
            CultureInfo culture = new CultureInfo("es-AR"); 
            string importeTotalFormatted = importeTotal.ToString("C", culture); 
            tf.DrawString($"Importe Total: {importeTotalFormatted}", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);

            document.Save(pdfFileName);

            System.Diagnostics.Process.Start(pdfFileName);
        }
    }
}
