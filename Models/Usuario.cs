using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace webMED.Models
{
    public class Usuario
    {
        public string ID_Usuario { get; set; }
        public string Password { get; set; }
        public string Tipo { get; set; }

        public void CargarUsuario(Usuario usr)
        {
            if (usr == null)
            {
                ID_Usuario = "";
                Password = "";
                Tipo = "";
            }
            else
            {
                ID_Usuario = usr.ID_Usuario;
                Password = usr.Password;
                Tipo = usr.Tipo;
            }
        }

        public bool ValidarUsuario(string usr)
        {

            return ID_Usuario.Trim().Equals(usr.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public bool ValidarPassword(string pwd)
        {
            return Password.Trim().Equals(pwd.Trim());
        }
    }
}
