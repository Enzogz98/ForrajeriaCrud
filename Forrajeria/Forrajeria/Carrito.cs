using CapaLogica;
using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;

namespace Forrajeria
{
    public partial class Carrito : Form
    {
        private CLN_Carrito objCarrito_CLN;
        private DataTable Tabla;
        private DataTable Tabla2;
        private DataTable Carro;
        private string nombre;
        private string direccion;
        private string telefono;
        private int indice;
        private int idProd;
        decimal precio;
        int cantidad;
        int c;
        decimal a;
        decimal subtotal;
        int stock;
        int stockTotal;
        int stockeditar;


        public Carrito()
        {
            InitializeComponent();
            objCarrito_CLN = new CLN_Carrito();
            Tabla = new DataTable();
            Tabla2 = new DataTable();
            Carro = new DataTable();
            c = 0;
            a = 0;
            nombre = "";
            direccion = "";
            telefono = "";
            MostrarProductos();
        }


        private void MostrarProductos()
        {
            Tabla.Clear();
            Tabla = objCarrito_CLN.ProductosDisponibles();
            dgvProductos.DataSource = Tabla;

        }

        private void Filtrar()
        {
            string texto = txtBuscar.Text;
            if (int.TryParse(texto, out int numero))
            {
                Tabla.Clear();
                Tabla = objCarrito_CLN.FiltrarId(numero);
                dgvProductos.DataSource = Tabla;
            }
            else
            {
                Tabla.Clear();
                Tabla = objCarrito_CLN.FiltrarText(texto);
                dgvProductos.DataSource = Tabla;
            }


        }
        private void CantidadChange()
        {
            string texto = txtCantidad.Text;
            if (texto == "")
            {
                texto = "0";
            }
            if (int.TryParse(texto, out int numero))
            {
                if (numero > stock)
                {
                    MessageBox.Show("La cantidad ingresada no puede superar al stock del producto");
                }
                else
                {

                    subtotal = objCarrito_CLN.PrecioXcantidad(numero, precio);
                    lblSubTotal.Text = "$" + Convert.ToString(subtotal);
                }
            }
        
        }
        private void CantidadChange2()
        {
            string texto = txtCantidadEditar.Text;
            
            
            //MessageBox.Show(stockTotal.ToString());
            if (texto == "")
            {
                texto = "0";
                stockTotal = (Convert.ToInt32(texto)) + stock;
            }
            else if (int.TryParse(texto, out int numero))
            {
                stockTotal = (Convert.ToInt32(texto)) + stock;
                if (numero > stockTotal)
                {
                    MessageBox.Show("La cantidad ingresada no puede superar al stock del producto");
                }
                else
                {

                    subtotal = objCarrito_CLN.PrecioXcantidad(numero, precio);
                    lblSubTotal.Text = "$" + Convert.ToString(subtotal);
                    
                }
            }

        }
        private void EditarCarro()
        {
            string texto = txtCantidadEditar.Text;
            if (int.TryParse(texto, out int numero))
            {
                if (numero != stockeditar)
                {
                    if (numero > stockeditar)
                    {
                        objCarrito_CLN.RestarStock(idProd, numero - stockeditar);
                        Carro = objCarrito_CLN.EditarCarrito(indice,Convert.ToDecimal(lblPrecio.Text),numero);
                        dgvCarrito.DataSource = Carro;


                    }
                    else if (stockeditar > numero)
                    {
                        objCarrito_CLN.RestaurarStock(idProd, stockeditar - numero);
                        Carro = objCarrito_CLN.EditarCarrito(indice, Convert.ToDecimal(lblPrecio.Text), numero);
                        dgvCarrito.DataSource = Carro;
                    }
                    MostrarProductos();
                    MessageBox.Show("Se aplicaron los cambios al carrito");
                   
                    c = 0;
                    a = 0;
                    foreach (DataRow row in Carro.Rows)
                    {
                        a += (decimal)row["Total"];
                        c += (int)row["Cantidad"];
                    }
                    lblTotal.Text = "Cantidad Productos: " + c;
                    lblTotalCarrito.Text = "$" + a;




                }

            }
        }
        private void EliminarItemCarro()
        {
            Carro = objCarrito_CLN.EliminarItemCarro(indice);
            MessageBox.Show("se elimino el producto del carrito");
            MostrarProductos();
            dgvCarrito.DataSource = Carro;
            c = 0;
            a = 0;
            foreach (DataRow row in Carro.Rows)
            {
                a += (decimal)row["Total"];
                c += (int)row["Cantidad"];
            }
            lblTotal.Text = "Cantidad Productos: " + c;
            lblTotalCarrito.Text = "$" + a;

        }

        private void AgregarAlCarrito(int IDProducto)
        {
            if (lblNombreProd.Text == "Producto" && lblSubTotal.Text == "$0.00")
            {
                MessageBox.Show("debe cargar un producto");
            }
            else
            {
                string texto = txtCantidad.Text;
                if (int.TryParse(texto, out int numero))
                {
                    if (numero > stock)
                    {
                        MessageBox.Show("La cantidad ingresada no puede superar al stock del producto");
                    }
                    else
                    {

                        Carro = objCarrito_CLN.DetalleCarrito(IDProducto, numero);
                        objCarrito_CLN.RestarStock(IDProducto, numero);
                        MostrarProductos();
                        c += numero;
                        a = 0;
                        foreach (DataRow row in Carro.Rows)
                        {
                            a += (decimal)row["Total"];
                        }
                        lblTotal.Text = "Cantidad Productos: " + c;
                        lblTotalCarrito.Text = "$" + a;
                        dgvCarrito.DataSource = Carro;

                    }

                }
                else
                {
                    MessageBox.Show("Ingrese una Cantidad Valida");
                }
            }
            
        }



        private void limpiarCampos()
        {
            txtBuscar.Text = "";
            lblNombreProd.Text = "Producto";
            txtCantidad.Text = "1";
            lblSubTotal.Text = "$0.00";
            lblPrecio.Text = "Precio";
            btnAgregarCarro.Visible = true;
            btnAplicar.Visible = false;
            btnEliminar.Visible = false;
            btnCancelar.Visible = false;
            txtCantidadEditar.Visible = false;
            txtCantidad.Visible = true;
            btnAgregarCarro.Enabled=false;
        }


        private void PagarCarrito()
        {
            decimal total = a;
            int ClienteID = objCarrito_CLN.InsertCliente(txtNombreCliente.Text, txtDireccion.Text, txtTelefono.Text);
            int VentaID = objCarrito_CLN.insertVenta(dtpFechaVenta.Value.ToString(),ClienteID, total);
            foreach (DataRow row in Carro.Rows)
            {
                objCarrito_CLN.insertDetallesVenta(VentaID,(int)row["Codigo"],(int)row["Cantidad"], (decimal)row["Precio"]);
            }

            //MessageBox.Show("Cliente,Venta y detalles insertados");
            GenerarFactura(VentaID);
            limpiarCampos();
            MostrarProductos();
            lblTotal.Text = "Cantidad Productos: 0" ;
            lblTotalCarrito.Text = "$0.00" ;
            Carro.Clear();
            dgvCarrito.DataSource = Carro;

        }

       

        ////////////////////////////////////////////////////////////////////////////////////////

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            nombre=txtNombreCliente.Text;
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            direccion= txtDireccion.Text;
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            telefono=txtTelefono.Text;
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
            txtCantidad.Text = "1";
            if (indice > -1)
            {
                btnAgregarCarro.Enabled = true;    
                idProd = Convert.ToInt32(dgvProductos.Rows[indice].Cells[0].Value);
                lblNombreProd.Text = dgvProductos.Rows[indice].Cells[1].Value.ToString();
                lblPrecio.Text ="$"+ dgvProductos.Rows[indice].Cells[4].Value.ToString();
                stock = Convert.ToInt32(dgvProductos.Rows[indice].Cells[3].Value);
                precio = Convert.ToDecimal(dgvProductos.Rows[indice].Cells[4].Value);
                cantidad = Convert.ToInt32(txtCantidad.Text);
                subtotal = objCarrito_CLN.PrecioXcantidad(cantidad,precio);
                lblSubTotal.Text ="$"+ Convert.ToString(subtotal);
                btnAgregarCarro.Visible = true;
                txtCantidad.Visible = true;
                txtCantidadEditar.Visible = false;
                btnAplicar.Visible = false;
                btnEliminar.Visible = false;
                btnCancelar.Visible = false;
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            CantidadChange();
        }

       

        private void dgvCarrito_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
            if (indice > -1)
            {
                btnAgregarCarro.Visible = false;
                idProd = Convert.ToInt32(dgvCarrito.Rows[indice].Cells[0].Value);
                lblNombreProd.Text = dgvCarrito.Rows[indice].Cells[1].Value.ToString();
                lblPrecio.Text = dgvCarrito.Rows[indice].Cells[2].Value.ToString();
                precio = Convert.ToDecimal(dgvCarrito.Rows[indice].Cells[2].Value);
                cantidad = Convert.ToInt32(dgvCarrito.Rows[indice].Cells[3].Value);
                stock = objCarrito_CLN.StockActual(idProd);
                txtCantidadEditar.Text = dgvCarrito.Rows[indice].Cells[3].Value.ToString();
                stockeditar=Convert.ToInt32(dgvCarrito.Rows[indice].Cells[3].Value);
                txtCantidadEditar.Visible = true;
                btnAplicar.Visible=true;
                btnEliminar.Visible=true;
                btnCancelar.Visible=true;
               
            }
        }

        private void txtCantidadEditar_TextChanged(object sender, EventArgs e)
        {
            CantidadChange2();

        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            //actual
                EditarCarro();
                limpiarCampos();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            //actual
            EliminarItemCarro();
            limpiarCampos();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            //actual
            limpiarCampos();

        }

        private void btnAgregarCarro_Click_1(object sender, EventArgs e)
        {
            //actual
           
                AgregarAlCarrito(idProd);

                limpiarCampos();
            
        }

        private void btnPagar_Click_1(object sender, EventArgs e)
        {
            //actual
            if (string.IsNullOrEmpty(txtNombreCliente.Text))
            {
                System.Media.SystemSounds.Beep.Play();
                toolTip1.Show("Este campo es obligatorio.", txtNombreCliente, new Point(txtNombreCliente.Width + 1, 0), 1500);
            }
            else
            {
                toolTip1.Hide(txtNombreCliente);
                System.Media.SystemSounds.Beep.Play();
                DialogResult result = MessageBox.Show("¿Finalizar la Venta?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    PagarCarrito();
                }
               
                    

            }

        }

        


        private void GenerarFactura(int VentaID)
        {

            string cliente = nombre;
            string direccionCliente = direccion;
            string telefonoCliente = telefono;

            int numeroFactura = VentaID; //objCarrito_CLN.ObtenerNumeroFactura()

            DataTable detallesFactura = new DataTable();
            detallesFactura.Columns.Add("Producto");
            detallesFactura.Columns.Add("Cantidad");
            detallesFactura.Columns.Add("Precio unitario");
            detallesFactura.Columns.Add("Subtotal");
            decimal importeTotal = 0;
            foreach (DataRow row in Carro.Rows)
            {
                string producto = row["Nombre"].ToString();
                int cantidad = (int)row["Cantidad"];
                decimal precioUnitario = (decimal)row["Precio"];
                decimal subtotal = cantidad * precioUnitario;

                detallesFactura.Rows.Add(producto, cantidad, precioUnitario, subtotal);
                importeTotal += subtotal;
            }
           
            Imprimir(numeroFactura,cliente,direccionCliente,telefonoCliente,detallesFactura,importeTotal);
            

        }


        private void Imprimir(int numeroFactura, string cliente,string direccionCliente,string telefonoCliente,DataTable detallesFactura,decimal importeTotal)
        {
            string pdfFileName = "factura.pdf"; 

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Factura";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 12);

            XTextFormatter tf = new XTextFormatter(gfx);
            int yPosition = 50;


            tf.DrawString($"Factura #{numeroFactura}", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
            yPosition += 17;
            tf.DrawString($"Cliente: {cliente}", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
            yPosition += 13;
            tf.DrawString($"Dirección: {direccionCliente}", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
            yPosition += 13;
            tf.DrawString($"Teléfono: {telefonoCliente}", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
            yPosition += 20;
            tf.DrawString($"Productos: ", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);

            yPosition += 17;
            foreach (DataRow row in detallesFactura.Rows)
            {
                string producto = row["Producto"].ToString();
                string cantidad = row["Cantidad"].ToString();
                string precioUnitario = row["Precio unitario"].ToString();
                string subtotal = row["Subtotal"].ToString();

                tf.DrawString(producto, font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
                yPosition += 13;
                tf.DrawString(cantidad, font, XBrushes.Black, new XRect(250, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
                tf.DrawString(precioUnitario, font, XBrushes.Black, new XRect(350, yPosition, page.Width, page.Height), XStringFormats.TopLeft);
                tf.DrawString(subtotal, font, XBrushes.Black, new XRect(450, yPosition, page.Width, page.Height), XStringFormats.TopLeft);

                yPosition += 15;
            }

            yPosition += 20;
            CultureInfo culture = new CultureInfo("es-AR");
            string importeTotalFormatted = importeTotal.ToString("C", culture);
            tf.DrawString($"Importe Total: {importeTotalFormatted}", font, XBrushes.Black, new XRect(50, yPosition, page.Width, page.Height), XStringFormats.TopLeft);

            document.Save(pdfFileName);

            System.Diagnostics.Process.Start(pdfFileName);
        }

        private void btnCancelarCarro_Click_1(object sender, EventArgs e)
        {
            //actual

            limpiarCampos();

            lblTotal.Text = "Cantidad Productos: 0";
            lblTotalCarrito.Text = "$0.00";
            foreach (DataRow row in Carro.Rows)
            {
                int prodID;
                int cant;
                prodID = Convert.ToInt32(row["Codigo"]);
                cant = Convert.ToInt32(row["Cantidad"]);
                objCarrito_CLN.RestaurarStock(prodID, cant);
            }
            Carro.Clear();
            dgvCarrito.DataSource = Carro;
            MostrarProductos();
        }

        
    }
}
