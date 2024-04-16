using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Forrajeria
{
    public partial class Usuarios : Form
    {

        private CLN_Usuarios objUsuarios_CLN;
        private DataTable Tabla;
        private string nombreUsuario = "";
        private string pass = "";
        private string nombreCompleto = "";
        private string rol = "";
        private int indice;
        private int usuarioID;


        public Usuarios()
        {
            InitializeComponent();
            objUsuarios_CLN = new CLN_Usuarios();
            Tabla = new DataTable();
            MostrarUsuarios();
            cmbRol.Items.AddRange(new object[] { "Administrador", "Empleado" });

        }

        private void LimpiarCampos()
        {
            gbUsuario.Text = "Agregar Usuario";
            txtNombreUsuario.Text = "";
            txtPass.Text = "";
            txtNombreCompleto.Text = "";
            cmbRol.SelectedIndex = -1;
            btnAgregar.Visible = true;
            btnAct.Visible = false;
            btnEliminar.Visible = false;
        }

        public void MostrarUsuarios()
        {
            Tabla.Clear();
            Tabla = objUsuarios_CLN.ObtenerUsuarios();
            dgvUsuarios.DataSource = Tabla;
        }

        private void ObtenerUsuario()
        {
            nombreUsuario = txtNombreUsuario.Text;
            pass = txtPass.Text;
            nombreCompleto = txtNombreCompleto.Text;
            if (cmbRol.SelectedIndex != -1)
            {
                rol = cmbRol.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un rol.");
                return; 
            }
        }

        private void AgregarUsuario()
        {
            ObtenerUsuario();
            objUsuarios_CLN.CrearUsuario(nombreUsuario, pass, nombreCompleto, rol);
            MessageBox.Show("Usuario agregado correctamente");
            MostrarUsuarios();
            LimpiarCampos();
        }

        private void ModificarUsuario()
        {
            ObtenerUsuario();
            objUsuarios_CLN.ActualizarUsuario(usuarioID, nombreUsuario, pass, nombreCompleto, rol);
            MessageBox.Show("Se Guardaron los cambios");
            MostrarUsuarios();
            LimpiarCampos();
        }

        private void EliminarUsuario()
        {
            objUsuarios_CLN.EliminarUsuario(usuarioID);
            MessageBox.Show("Usuario eliminado correctamente");
            MostrarUsuarios();
            LimpiarCampos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarUsuario();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarUsuario();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnAct_Click(object sender, EventArgs e)
        {
            ModificarUsuario();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
            if (indice > -1)
            {
                btnAct.Visible = true;
                btnEliminar.Visible = true;
                btnAgregar.Visible = false;
                usuarioID = Convert.ToInt32(dgvUsuarios.Rows[indice].Cells["UsuarioID"].Value);
                txtNombreUsuario.Text = Convert.ToString(dgvUsuarios.Rows[indice].Cells["NombreUsuario"].Value);
                txtPass.Text = Convert.ToString(dgvUsuarios.Rows[indice].Cells["Pass"].Value); 
                txtNombreCompleto.Text = Convert.ToString(dgvUsuarios.Rows[indice].Cells["NombreCompleto"].Value);
                cmbRol.SelectedItem = Convert.ToString(dgvUsuarios.Rows[indice].Cells["Rol"].Value);
            }
        }
    }
}
