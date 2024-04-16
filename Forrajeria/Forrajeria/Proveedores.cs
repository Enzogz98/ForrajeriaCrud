using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogica;

namespace Forrajeria
{
    public partial class Proveedores : Form

    {
        private CLN_Proveedor objProovedores_CLN;
        private DataTable Tabla;
        private string nombre = "";
        private string direccion = "";
        private string telefono = "";
        private int indice;
        private int idProv;
        public Proveedores()
        {
            InitializeComponent();
            objProovedores_CLN = new CLN_Proveedor();
            Tabla = new DataTable();
            MostrarProovedor();
        }

        private void limpiarCampos()
        {
            gbProveedor.Text = "Agregar Proveedor";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            btnAgregar.Visible = true;
            btnAplicar.Visible = false;
            btnEliminar.Visible = false;
        }
        public void MostrarProovedor()
        {
            Tabla.Clear();
            Tabla = objProovedores_CLN.consultarProveedores();
            dgvProovedores.DataSource = Tabla;

        }

        private void ObtenerProovedor()
        {
            nombre = txtNombre.Text;
            direccion = txtDireccion.Text;
            telefono = txtTelefono.Text;
            
            
        }
        private void AgregarProovedor()
        {
            ObtenerProovedor();
            objProovedores_CLN.AgregarProovedor(nombre, direccion, telefono);
            MessageBox.Show("Proovedor agregado correctamente");
            MostrarProovedor();
            limpiarCampos();
        }
        private void ModificarProovedor()
        {
            ObtenerProovedor();
            objProovedores_CLN.ModificarProovedor(idProv, nombre, direccion, telefono);
            MessageBox.Show("Se Guardaron los cambios");
            MostrarProovedor();
            limpiarCampos();
        }

        private void EliminarProovedores()
        {
            objProovedores_CLN.EliminarProovedores(idProv);
            MessageBox.Show("Proovedor eliminado correctamente");
            MostrarProovedor();
            limpiarCampos();

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////

       


        private void dgvProovedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            indice = e.RowIndex;
            if (indice > -1)
            {
                gbProveedor.Text = "Editar Proveedor";
                btnAplicar.Visible = true;
                btnEliminar.Visible = true;
                btnAgregar.Visible = false;
                idProv = Convert.ToInt32(dgvProovedores.Rows[indice].Cells[0].Value);
                txtNombre.Text = Convert.ToString(dgvProovedores.Rows[indice].Cells[1].Value);
                txtDireccion.Text = Convert.ToString(dgvProovedores.Rows[indice].Cells[2].Value);
                txtTelefono.Text = Convert.ToString(dgvProovedores.Rows[indice].Cells[3].Value);
            }

        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            ModificarProovedor();
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            AgregarProovedor();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            EliminarProovedores();
        }
    }
}
