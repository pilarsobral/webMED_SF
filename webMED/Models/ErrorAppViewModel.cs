using System;

namespace webMED.Models
{
    public class ErrorAppViewModel
    {
        public string RequestId { get; set; }
        public string Encabezado { get; set; }
        public string Nivel { get; set; }

        public string Descripcion { get; set; }

        public string Retorno_Controller { get; set; }
        public string Retorno_Action { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
