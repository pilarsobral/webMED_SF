using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMED.Models
{
    public class PacienteCondicion
    {
        public string ID_Condicion { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta{ get; set; }
        public string Observaciones { get; set; }
    }
}
