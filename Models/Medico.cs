using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMED.Models
{
    public class Medico : Usuario
    {
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido{ get; set; }
        public string Especialidad { get; set; }


        public Medico()
        {


            Matricula = "";
            Nombre = "";
            Apellido = "";
            Especialidad = ""; 
    }
    public Medico (Usuario usr)
        {
            this.ID_Usuario = usr.ID_Usuario;
            this.Tipo = usr.Tipo; 


        }
    }
}
