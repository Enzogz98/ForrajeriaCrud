using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forrajeria
{
    public partial class Productos : Form
    {
        private CLN_Productos objProductos_CLN;
        private DataTable Tabla;
        private DataTable tablaCombobox;
        private string nom;
        private decimal precio;
        private string descripcion;
        private int stock;
        private int idProv;
        private int idProd;
        private int indice;
        private string buscar;
        private decimal importe;
        private string fecha;

        public Productos()
        {
            InitializeComponent();
            objProductos_CLN = new CLN_Productos();
            Tabla = new DataTable();
            tablaCombobox = new DataTable();
            nom ="";
            precio=0;
            descripcion="";
            stock=0;
            buscar = "";
            MostrarProductos();
            CargarCombo();
        }
        private void limpiarCampos()
        {
            txtNombre.Text = "";
            txtPrecio.Text = "0";
            txtStock.Text = "0";
            txtDescripcion.Text = "";
            txtProveedor.Text = "1";
            gbNuevoProd.Text= "Nuevo Producto";
            //btnAplicar.Visible= true;
            //btnEliminar.Visible= true;
            //btnAgregar.Visible= false;
            txtBuscar.Text = "";
            txtImporte.Text = "";
            rbCompra.Checked = false;
            rbModificar.Checked = true;
            CargarCombo();

        }
        private void MostrarProductos()
        {
            Tabla.Clear();
            Tabla = objProductos_CLN.MostrarProductos();
            dgvProductos.DataSource = Tabla;

        }
        private void Filtrar()
        {
            string texto = txtBuscar.Text;
            if (int.TryParse(texto, out int numero))
            {
                Tabla.Clear();
                Tabla = objProductos_CLN.FiltrarId(numero);
                dgvProductos.DataSource = Tabla;
            }
            else
            {
                Tabla.Clear();
                Tabla = objProductos_CLN.FiltrarText(texto);
                dgvProductos.DataSource = Tabla;
            }
            
            
        }

        private void obtenerProducto()
        {
            nom=txtNombre.Text;
            precio= Convert.ToDecimal(txtPrecio.Text);
            descripcion= txtDescripcion.Text;
            stock = Convert.ToInt32(txtStock.Text);
            //if (txtProveedor.SelectedValue != null)
            //{
            //    string cboindice = txtProveedor.SelectedValue.ToString();
            //    if (cboindice != "System.Data.DataRowView")
            //    {
            //        idProv = Convert.ToInt32(cboindice);
            //    }
            //}

        }
        private void AgregarProducto()
        {
            obtenerProducto();
            obtenerCompra();
            objProductos_CLN.AgregarProductos(nom,precio,descripcion,stock,idProv);
            objProductos_CLN.insertarCompra(fecha, idProv, importe);
            MessageBox.Show("Productos agregados correctamente");
            MostrarProductos();
            limpiarCampos();

        }

       

        

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
            if (indice > -1)
            {
                gbNuevoProd.Text = "Editar Producto";
                btnAgregar.Visible = false;
                btnAplicar.Visible = true;
                btnEliminar.Visible = true;
                idProd = Convert.ToInt32(dgvProductos.Rows[indice].Cells[0].Value);
                txtNombre.Text = Convert.ToString(dgvProductos.Rows[indice].Cells[1].Value);
                txtDescripcion.Text = Convert.ToString(dgvProductos.Rows[indice].Cells[2].Value);
                txtPrecio.Text = Convert.ToString(dgvProductos.Rows[indice].Cells[3].Value);
                txtStock.Text = Convert.ToString(dgvProductos.Rows[indice].Cells[4].Value);
                txtProveedor.Text = Convert.ToString(dgvProductos.Rows[indice].Cells[5].Value);
            }
        }

        private void EditarProductos()
        {
            obtenerProducto();
            objProductos_CLN.EditarProductos(idProd,nom, precio, descripcion, stock, idProv);
            if (rbCompra.Checked==true)
            {
                obtenerCompra();
                objProductos_CLN.insertarCompra(fecha, idProv, importe);
            }
            MessageBox.Show("Se Guardo correctamente los cambios");
            MostrarProductos();
            limpiarCampos();

        }
        private void EliminarProductos()
        {

            objProductos_CLN.EliminarProductos(idProd);
            MessageBox.Show("Se elimino correctamente");
            MostrarProductos();
            limpiarCampos();
        }
        private void RestaurarBusqueda()
        {
            txtBuscar.Text = "";
            MostrarProductos();
        }

        public void CargarCombo()
        {
            tablaCombobox.Clear();
            tablaCombobox = objProductos_CLN.ComboProveedor();
            txtProveedor.DataSource = tablaCombobox;
            txtProveedor.DisplayMember = "Nombre";
            txtProveedor.ValueMember = "ProveedorID"; 

        }
        public void obtenerCompra()
        {
            fecha = Convert.ToString(dtFecha.Value);
            importe= Convert.ToDecimal(txtImporte.Text);
        }


        /////////////////////////////////////////////////////////////////
        
        

        

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            RestaurarBusqueda();
        }

        private void txtProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtProveedor.SelectedValue != null)
            {
                string cboindice= txtProveedor.SelectedValue.ToString();
                if(cboindice != "System.Data.DataRowView")
                {
                    idProv = Convert.ToInt32(cboindice);
                }
            }
        }

        private void lblNombre_Click(object sender, EventArgs e)
        {

        }

        private void lblDescripcion_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            EliminarProductos();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            EditarProductos();
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            AgregarProducto();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void rbCompra_CheckedChanged(object sender, EventArgs e)
        {
            lblDate.Visible = true;
            lblImporte.Visible = true;
            btnAgregar.Visible = true; 
            txtImporte.Visible = true;
            dtFecha.Visible = true; 
            txtStock.Enabled = true;
            btnEliminar.Visible = false;
            btnAplicar.Visible = false; 

        }

        private void rbModificar_CheckedChanged(object sender, EventArgs e)
        {
            lblDate.Visible = false;
            lblImporte.Visible = false; 
            btnAgregar.Visible = false; 
            txtImporte.Visible = false; 
            dtFecha.Visible=false;
            txtStock.Enabled = false;
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter presionado no es un número, no es un punto y no es una coma, ni es la tecla de retroceso
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            // Expresión regular para permitir números y comas o puntos como separadores decimales
            string pattern = @"^\d+([\.,]\d{0,2})?$";
            Regex regex = new Regex(pattern);

            string currentText = txtImporte.Text.Insert(txtImporte.SelectionStart, e.KeyChar.ToString());

            // Valida el carácter ingresado con la expresión regular
            if (!regex.IsMatch(currentText))
            {
                e.Handled = true;
            }
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }
    }
}
