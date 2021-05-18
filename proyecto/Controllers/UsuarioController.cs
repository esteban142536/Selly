using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index(String email, String clave)
        {
            usuario user;

            IServiceUsuario iserviseUsuario = new ServiceUsuario();
            user = iserviseUsuario.logIn(email, clave);

            if (user==null)
            {
                return View(user);
            }

            return PrevalenceData(user);
            //return RedirectToAction("PrevalenceData", "Usuario");
        }

        [HttpPost]
        public ActionResult PrevalenceData(usuario usu)
        {
            return View(usu);
        }
    }
}