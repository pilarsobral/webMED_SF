using System;

namespace webMED.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        
        public string Descripcion { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
