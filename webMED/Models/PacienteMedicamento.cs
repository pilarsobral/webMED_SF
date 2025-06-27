using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMED.Models
{
    public class PacienteMedicamento
    {
        public string ID_Medicamento { get; set; }
        public string ID_Droga { get; set; }
        public string DescripcionMedicamento { get; set; }
        public string DescripcionDroga { get; set; }
        public string Estado { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta{ get; set; }
        public string Observaciones { get; set; }
    }
}
