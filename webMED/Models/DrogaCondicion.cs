using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMED.Models
{
    public class DrogaCondicion
    {
        public string ID_Droga{ get; set; }
        public string ID_Condicion { get; set; }
        public string Estado { get; set; }
        public string DescripcionDroga { get; set; }
        public string DescripcionCondicion{ get; set; }
    }
}
