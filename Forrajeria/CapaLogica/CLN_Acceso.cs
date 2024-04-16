using CapaAccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLN_Acceso
    {
        private CAD_Login objCADLogin;
        private DataTable Tabla; 
        public CLN_Acceso() 
        {
            objCADLogin = new CAD_Login();
            Tabla = new DataTable();
        }
        public DataTable Acceso(string User, string Pass)
        {
            Tabla=objCADLogin.Acceso(User, Pass);
            return Tabla;
        }
    }
}
