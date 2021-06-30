using ApplicationCore.Services;
using Infraestructure;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class ContactoController : Controller
    {
        IServiseContactos serviceContacto = new ServiseContactos();
        IServiseProveedor serviseProveedor = new ServiseProveedor();

        [HttpPost]
        public ActionResult Save(contactos contactos, String[] proveedor)
        {

            contactos.idProveedor = int.Parse(proveedor[0]);
            if (contactos.correo == null || contactos.nombre == null|| contactos.numero==0)
            {
                ViewBag.idProveedor = listaProveedor(contactos.idProveedor);
                return View("Index", contactos);
            }


            serviceContacto.GuardarContactos(contactos);
            return RedirectToAction("Index");
        }
        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]
        public ActionResult Index()
        {
            ViewBag.idProveedor = listaProveedor();
            return View();
        }
        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]
        public ActionResult EditarContacto(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                contactos contactos = serviceContacto.obtenerContactoID(id.Value);
                if (contactos == null)
                {
                    TempData["Message"] = "No existe el contacto solicitado";
                    return RedirectToAction("Index");
                }

                ViewBag.idProveedor = listaProveedor(contactos.idProveedor);
                return View(contactos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        //Listas para llenar los combos
        private SelectList listaProveedor(int idProveedor = 0)
        {
            IEnumerable<proveedor> listaProveedor = serviseProveedor.listadoProveedor();
            return new SelectList(listaProveedor, "id", "nombreEmpresa", idProveedor);
        }
    }
}