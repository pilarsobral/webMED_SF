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
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly DBService _DBService;

        public LoginController(DBService dbService, ILogger<LoginController> logger)
        {
            _DBService = dbService; 
            _logger = logger;
        }
        public IActionResult Ingresar(Usuario modelo)
        {
            if (modelo == null)
                return RedirectToAction("Login");
            else if (modelo.ID_Usuario  == null)
                return RedirectToAction("Login");

            Usuario regUsuario = new Usuario(); 
            regUsuario.CargarUsuario(_DBService.ObtenerUsuario(modelo.ID_Usuario)); 

            if (!regUsuario.ValidarUsuario( modelo.ID_Usuario))
                return RedirectToAction("ErrorUsuario");

            if (!regUsuario.ValidarPassword(modelo.Password))
                return RedirectToAction("ErrorPassword");

            if (regUsuario.Tipo.Trim().Equals("MEDICO"))
                return RedirectToAction("SuccessMedico", regUsuario );
            return View();

        }
        public IActionResult SuccessMedico(Usuario usr)
        {
            MDIngreso md = new MDIngreso(); 
            md.Medico = new Medico(usr);
            _DBService.CargarMedico(md.Medico);

            return View("MD_Ingreso", md);
        }

        public IActionResult ErrorPassword()
        {
            return View("ErrorApp", new ErrorAppViewModel 
                { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Encabezado = "Password Invalida",
                Nivel = "ERROR",
                Retorno_Controller = "Home",
                Retorno_Action = "Login",
                Descripcion = "Verifique que sea correcta" });
        }



        public IActionResult ErrorUsuario()
        {
            return View("ErrorApp", new ErrorAppViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Encabezado = "Usuario Invalido",
                Nivel = "ERROR",
                Retorno_Controller = "Home",
                Retorno_Action = "Login",
                Descripcion = "No existe el Usuario dado de alta. Ingrese a la siguiente pagina para registrarse."
            });
        }
    }
}
