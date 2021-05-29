using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Web.Mvc;

namespace proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        IServiceUsuario iserviseUsuario = new ServiceUsuario();
        IServiceTipoUsuario iserviseTipoUsuario = new ServiceTipoUsuario();

        // GET: Usuario
        public ActionResult Index(String email, String clave)
        {
            usuario user;

            user = iserviseUsuario.logIn(email, clave);

            if (user==null)
            {
                return View(user);
            }

            Session.Add("UserType", iserviseTipoUsuario.obtenerPermisos(user.idTipoUsuario));

         
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


         tipoUsuario tu=  iserviseTipoUsuario.asignarPermisos(usu.idTipoUsuario);
            usu.idTipoUsuario = tu.id;
            iserviseUsuario.SignIn(usu);

           return View();
        }

    }
}