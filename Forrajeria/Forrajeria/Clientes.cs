using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forrajeria
{
    public partial class Clientes : Form
    {
        private CLN_Clientes objClientesCLN;
        private DataTable Tabla;
        private string nombre = "";
        private string direccion = "";
        private string telefono = "";
        private int indice;
        private int idCliente;

        public Clientes()
        {
            InitializeComponent();
            objClientesCLN = new CLN_Clientes();
            Tabla = new DataTable();
            mostrarClientes();
        }
        public void mostrarClientes() 
        {
            Tabla.Clear();
            Tabla=objClientesCLN.ObtenerClientes();
            dgvClientes.DataSource = Tabla;
        }
        private void limpiarCampos()
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            btnAgregar.Visible = true;
            btnAct.Visible = false;
            btnDelete.Visible = false;

        }
        private void obtenerClientes()
        {
            nombre=txtNombre.Text;
            direccion=txtDireccion.Text;
            telefono = txtTelefono.Text;
        }
        private void agregarCliente()
        {
            obtenerClientes();
            objClientesCLN.InsertarCliente(nombre,direccion,telefono);
            MessageBox.Show("Cliente agregado correctamente");
            mostrarClientes();
            limpiarCampos();
        }
        private void ActualizarCliente()
        {
            obtenerClientes();
            objClientesCLN.ActualizarClienter(idCliente, nombre, direccion, telefono);
            MessageBox.Show("Se Guardaron los cambios");
            mostrarClientes() ;
            limpiarCampos();
        }

        private void EliminarCliente()
        {
            objClientesCLN.EliminarCliente(idCliente);
            MessageBox.Show("Cliente eliminado correctamente");
            mostrarClientes();
            limpiarCampos();

        }



        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            indice = e.RowIndex;
            if (indice > -1)
            {
                btnAct.Visible = true;
                btnDelete.Visible = true;
                btnAgregar.Visible = false;
                idCliente = Convert.ToInt32(dgvClientes.Rows[indice].Cells[0].Value);
                txtNombre.Text = Convert.ToString(dgvClientes.Rows[indice].Cells[1].Value);
                txtDireccion.Text = Convert.ToString(dgvClientes.Rows[indice].Cells[2].Value);
                txtTelefono.Text = Convert.ToString(dgvClientes.Rows[indice].Cells[3].Value);
            }
        }

        private void lblTel_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            EliminarCliente();
        }

        private void btnAct_Click_1(object sender, EventArgs e)
        {
            ActualizarCliente();
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            agregarCliente();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            limpiarCampos();
        }
    }
}
