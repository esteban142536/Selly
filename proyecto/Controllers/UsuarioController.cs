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
        public ActionResult Index()
        {
            usuario user = null;
            
            IServiceUsuario iserviseUsuario = new ServiceUsuario();
            user = iserviseUsuario.logIn("admin","123");


            return View(user);
        }
    }
}