using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMED.Models
{
    public class PacienteDroga
    {
        public decimal ID_GrupoSocio { get; set; }
        public decimal ID_Socio { get; set; }
        public string ID_Droga { get; set; }
        public string Estado { get; set; }
        public string DescripcionDroga { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta{ get; set; }


    }
}
