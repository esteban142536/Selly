using ApplicationCore.Services;
using Infraestructure.Models;
using System;
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
            Session.Add("User",user);

         
            return RedirectToAction("PrevalenceData", "Usuario", user);
        }

        
        public ActionResult PrevalenceData(usuario usu)
        {
            return View(usu);
        }


        //insert hacia usuario
        public ActionResult escritura(usuario usu) {

            if (usu.nombre == null) {
                return View();
            }

            IServiceUsuario iserviseUsuario = new ServiceUsuario();
            IServiceTipoUsuario iserviseTipoUsuario = new ServiceTipoUsuario();

         tipoUsuario tu=  iserviseTipoUsuario.asignarPermisos(usu.idTipoUsuario);
            usu.idTipoUsuario = tu.id;
            iserviseUsuario.SignIn(usu);

           return View();
        }

    }
}