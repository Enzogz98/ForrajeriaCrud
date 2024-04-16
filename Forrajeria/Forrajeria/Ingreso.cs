using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogica;

namespace Forrajeria
{
    public partial class Ingreso : Form
    {
        private CLN_Acceso objCLNLogin;
        private DataTable Tabla;
        private string user ;
        private string pass;
        private string nameUser;
        public Ingreso()
        {
            InitializeComponent();
            objCLNLogin = new CLN_Acceso();
            Tabla = new DataTable();
            user = "";
            pass = "";
            nameUser = "";
        }

        

       

        private void ObtenerDatos(string User, string Pass)
        {
            Tabla.Clear();
            Tabla = objCLNLogin.Acceso(User, Pass);
            bool ingresoCorrecto = false;

            foreach (DataRow rows in Tabla.Rows)
            {
                if ((User == Convert.ToString(rows["NombreUsuario"])) && (Pass == Convert.ToString(rows["Pass"])))
                {
                    ingresoCorrecto = true;
                    nameUser = Convert.ToString(rows["NombreCompleto"]);
                    break;
                }
           
            };

            if (ingresoCorrecto)
            {
                Menu mainMenu = new Menu();
                mainMenu.Show();
                this.Hide();
                //MessageBox.Show("Bienvenido " + nameUser);
            }
            else MessageBox.Show("Usuario o contraseña incorrecta");
            

        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);



        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnAcceso_Click(object sender, EventArgs e)
        {
            user = txtUser.Text;
            pass = txtPass.Text;
            ObtenerDatos(user, pass);
        }
    }
}
