using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using webMED.Models;

namespace webMED.Controllers
{
    public class MedicoController : Controller
    {
        private readonly ILogger<MedicoController> _logger;
        private readonly DBService _DBService;

        public MedicoController(DBService dbService, ILogger<MedicoController> logger)
        {
            _DBService = dbService; 
            _logger = logger;
        }
        public IActionResult Buscar(MDIngreso modelo)
        {

            string[] cmp = modelo.IDPaciente.Split('/');
            if (cmp.Length != 2)
                return RedirectToAction("ErrorPaciente_Formato");

            decimal grupo = 0;
            if (!decimal.TryParse(cmp[0].Trim(), out grupo))
                return RedirectToAction("ErrorPaciente_GrupoNum");

            decimal id = 0;
            if (!decimal.TryParse(cmp[1].Trim(), out id))
                return RedirectToAction("ErrorPaciente_ID");

            modelo.CargarPaciente(_DBService.ObtenerPaciente(grupo, id));

            modelo.CargarCondiciones(_DBService.ObtenerCondiciones(grupo, id));
            modelo.CargarMedicamentos(_DBService.ObtenerMedicamentos(grupo, id));
            modelo.CargarDrogas(_DBService.ObtenerDrogas(grupo, id));

            return View("MD_Paciente", modelo);

        }

        public IActionResult ErrorPaciente_ID()
        {
            return View("ErrorApp", new ErrorAppViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Encabezado = "Socio Invalido",
                Nivel = "ERROR",
                Retorno_Controller = "Login",
                Retorno_Action = "SuccessMedico",
                Descripcion = "El ID del Socio debe ser numerico. "
            });
        }
        public IActionResult ErrorPaciente_GrupoNum()
        {
            return View("ErrorApp", new ErrorAppViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Encabezado = "Socio Invalido",
                Nivel = "ERROR",
                Retorno_Controller = "Login",
                Retorno_Action = "SuccessMedico",
                Descripcion = "El Grupo del Socio debe ser numerico. "
            });
        }
        public IActionResult ErrorPaciente_Formato()
        {
            return View("ErrorApp", new ErrorAppViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Encabezado = "Socio Invalido",
                Nivel = "ERROR",
                Retorno_Controller = "Login",
                Retorno_Action = "SuccessMedico",
                Descripcion = "La cantidad de campos difiere de lo solicitado. El formato debe ser GRUPO SOCIO / ID SOCIO  "
            });
        }
        public IActionResult ErrorPaciente()
        {
            return View("ErrorApp", new ErrorAppViewModel 
                { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Encabezado = "Socio Invalido",
                Nivel = "ERROR",
                Retorno_Controller = "Login",
                Retorno_Action = "SuccessMedico",
                Descripcion = "Controle que el ID sea correcto, el formato es GRUPO SOCIO / ID " });
        }
        [HttpGet]
        public IActionResult ObtenerDrogas()
        {
            var drogas = _DBService.ObtenerDrogas()
                .Select(d => new { d.ID_Droga, d.Descripcion })
                .ToList();

            return Json(drogas);
        }
        [HttpPost]
        public IActionResult AgregarDrogaAjax([FromBody] PacienteDroga nuevo)
        {
            if (nuevo == null)
                return BadRequest();

            List<DrogaCondicion> Lista_dc = _DBService.ObtenerCondicionesDroga(nuevo.ID_Droga);
            List<PacienteCondicion> Lista_c = _DBService.ObtenerCondiciones(nuevo.ID_GrupoSocio, nuevo.ID_Socio); 

            foreach (var dc in Lista_dc)
            {
                foreach(var c in Lista_c )
                {
                    if (c.ID_Condicion.Trim().Equals(dc.ID_Condicion.Trim()))
                    {
                        return Json(new
                        {
                            retorno = "ER",
                            descripcionDroga = dc.DescripcionDroga,
                            descripcionCondicion = dc.DescripcionCondicion ?? ""
                        });
                    }
                }
                
            }

            nuevo.Estado = "Pendiente";

            _DBService.IngresarDroga(nuevo); 

            // Devuelve el objeto creado como JSON para actualizar la tabla
            return Json(new
            {
                retorno = "OK", 
                idDroga = nuevo.ID_Droga,
                fechaDesde = nuevo.FechaDesde.ToString("dd/MM/yyyy"),
                fechaHasta = nuevo.FechaHasta.ToString("dd/MM/yyyy") ?? ""
            });
        }
    }
}
