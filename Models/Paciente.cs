using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMED.Models
{
    public class Paciente : Usuario
    {
        public decimal ID_GrupoSocio { get; set;  }
        public decimal ID_Socio { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }


        public Paciente()
        {
            ID_GrupoSocio =0;
            ID_Socio = 0;
            Nombre = "";
            Apellido = "";
            FechaNacimiento = new DateTime();
            FechaIngreso = new DateTime();
            Telefono = "";
            Mail = "";
        }


        public Paciente(Usuario usr)
        {
            this.ID_Usuario = usr.ID_Usuario;
            this.Tipo = usr.Tipo;
        }

        public void Cargar(Paciente pac)
        {
            ID_GrupoSocio = pac.ID_GrupoSocio;
            ID_Socio = pac.ID_Socio;
            Nombre = pac.Nombre;
            Apellido = pac.Apellido;
            FechaNacimiento = pac.FechaNacimiento;
            FechaIngreso = pac.FechaIngreso;
            Telefono = pac.Telefono;
            Mail = pac.Mail; 
        }
    }
}
