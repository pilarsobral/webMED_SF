using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMED.Models
{
    public class Droga
    {
        public string ID_Droga { get; set; }
        public string Descripcion { get; set; }


        public Droga()
        {


            ID_Droga = "";
            Descripcion= "";
            
    }
    public Droga(string id, string descripcion)
        {
            this.ID_Droga = id;
            this.Descripcion = descripcion; 


        }
    }
}
