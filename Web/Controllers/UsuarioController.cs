using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Web.Security;
using Web.Util;
using Web.Utils;
using Web.ViewModel;

namespace proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        IServiceUsuario repoUsua = new ServiceUsuario();
        IServiceTipoUsuario repoTipoUsua = new ServiceTipoUsuario();

        // GET: Usuario
        public ActionResult Index(String email, String clave)
        {

            if (email == null || clave == null)
            {
                return View();
            }

            usuario user;

            if (ModelState.IsValid)
            {
                user = repoUsua.logIn(email, clave);

                if (user == null)
                {
                    ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Inicio de sesión", "Error al autenticarse", SweetAlertMessageType.warning);
                    return View();
                }

                Session.Add("Usuario", user);
                return RedirectToAction("Index", "Home");
            }
            return View("Index");
        }


        public ActionResult PrevalenceData(usuario usu)
        {
            return View(usu);
        }


        //insert hacia usuario
        public ActionResult registro(usuario usu, String[] permiso)
        {
            ViewBag.idTipoUsuario = litadoPermisos();

            if (string.IsNullOrEmpty(usu.nombre) || string.IsNullOrEmpty(usu.apellidos) || string.IsNullOrEmpty(usu.clave) || string.IsNullOrEmpty(usu.email))
            {
                return View(usu);
            }
            usu.idTipoUsuario = 3;
            usu.esActivo = true;
            usu.idTipoUsuario =int.Parse(permiso[0]);

            // tipoUsuario tu = repoTipoUsua.asignarPermisos(usu.idTipoUsuario);
             usu.idTipoUsuario =3;


            repoUsua.SignIn(usu);

            TempData["NotificationMessage"] = Web.Util.SweetAlertHelper.Mensaje("Usuario", "Usuario creado", SweetAlertMessageType.success);
            return RedirectToAction("Index");
        }

        //cerrar sesion
        public ActionResult cerrarSesion()
        {
            try
            {
                Session["Usuario"] = null;
                Carrito.Instancia.eliminarCarrito();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult sinPermisos()
        {
            return View();
        }



        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]
        public ActionResult listaUsuario()
        {

            return View(repoUsua.listadoUsuario());
        }

        [CustomAuthorize((int)TipoUsuario.Administrador)]
        public ActionResult EditarUsuario(int id)
        {
            try
            {
                usuario user = repoUsua.obtenerUsuarioxID(id);
                ViewBag.esActivo = litadoEstado(user.esActivo);
                ViewBag.idTipoUsuario = litadoPermisos(user.idTipoUsuario);
                return View(user);

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult edit(usuario usu, String[] permiso, String[] estado)
        {
            try
            {


                usu.idTipoUsuario = int.Parse(permiso[0]);
                usu.esActivo = bool.Parse(estado[0]);
                repoUsua.editarUsuario(usu);

                return RedirectToAction("listaUsuario");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["NotificationMessage"] = Web.Util.SweetAlertHelper.Mensaje("Error", "Error al procesar los datos! " + ex.Message, SweetAlertMessageType.error);
                return RedirectToAction("EditarUsuario", usu.id);
            }
        }


        //carga los combos
        public SelectList litadoPermisos(int idPermiso = 0)
        {
            IEnumerable<tipoUsuario> listaPais = repoTipoUsua.listadoPermisos();
            return new SelectList(listaPais, "id", "permisoUsuario", idPermiso);
        }
        public SelectList litadoEstado(bool isActivo = true)
        {
            List<SelectListItem> listaEstados = new List<SelectListItem>();
            listaEstados.Add(new SelectListItem { Text = "Habilitado", Value = "true" });
            listaEstados.Add(new SelectListItem { Text = "Desabilitado", Value = "false" });
            return new SelectList(listaEstados, "Value", "Text", isActivo);
        }
    }
}