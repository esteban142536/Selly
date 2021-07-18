using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Reflection;
using System.Web.Mvc;
using Web.Util;
using Web.Utils;
using Web.ViewModel;

namespace proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        IServiceUsuario iserviseUsuario = new ServiceUsuario();
        IServiceTipoUsuario iserviseTipoUsuario = new ServiceTipoUsuario();

        // GET: Usuario
        public ActionResult Index(String email, String clave)
        {

            if(email == null || clave == null){ 
                return View();
            }

            usuario user;

            if (ModelState.IsValid)
            {
            user = iserviseUsuario.logIn(email, clave);

            if (user==null)
            {
                return View();
            }

            Session.Add("Usuario", user);

         
            return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Login", "Error al autenticarse", SweetAlertMessageType.warning);
            }
            return View("Index");
        }

        
        public ActionResult PrevalenceData(usuario usu)
        {
            return View(usu);
        }


        //insert hacia usuario
        public ActionResult registro(usuario usu) {

            if (usu.nombre == null) {
                return View();
            }


         tipoUsuario tu=  iserviseTipoUsuario.asignarPermisos(usu.idTipoUsuario);
            usu.idTipoUsuario = tu.id;
            iserviseUsuario.SignIn(usu);

           return View();
        }

        //cerrar sesion
        public ActionResult cerrarSesion()
        {
            try
            {
                Session["Usuario"] = null;
                Carrito.Instancia.eliminarCarrito();
            }
            catch(Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult sinPermisos()
        {

            return View();
        }

    }
}