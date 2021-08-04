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

namespace proyecto.Controllers
{
    public class ProveedorController : Controller
    {
        IServiseProveedor serviseProveedor = new ServiseProveedor();
        IServicePais servisepais = new ServicePais();

        [HttpPost]
        public ActionResult Save(proveedor prooveedor, String[] pais)
        {

          //  Valida si uno de sus datos esta vacios
            prooveedor.idPais = int.Parse(pais[0]);
            if (prooveedor.nombreEmpresa == null || prooveedor.direccion == null)
            {
                ViewBag.idPais = listaPais(prooveedor.idPais);
                return View("AgregarProveedor", prooveedor);
            }

            serviseProveedor.guardarProveedor(prooveedor);
            return RedirectToAction("index");
        }
        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]
        public ActionResult AgregarProveedor()
        {
            ViewBag.idPais = listaPais();
            return View();
        }

        public ActionResult DetalleProveedor(int id)
        {
            ViewBag.Title = "Datos del proveedor";
            return View(serviseProveedor.obtenerProveedorID(id));
        }
        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]
        public ActionResult EditarProveedor(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                proveedor proveedor = serviseProveedor.obtenerProveedorID(id.Value);
                if (proveedor == null)
                {
                    TempData["Message"] = "No existe el proveedor solicitado";
                    return RedirectToAction("Index");
                }

                ViewBag.idPais = listaPais();
                return View(proveedor);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Index()
        {
            ViewBag.listaNombres = serviseProveedor.nombreProveedor();
            return View(serviseProveedor.listadoProveedor());
        }

        public ActionResult ReporteProveedores()
        {
            ViewBag.Title = "Listado de provedores";
            return View(serviseProveedor.listadoProveedor());
        }

        public ActionResult buscarProveedorxNombre(string filtro)
        {
            IEnumerable<proveedor> lista = null;

            if (string.IsNullOrEmpty(filtro))
            {
                lista = serviseProveedor.listadoProveedor();
            }
            else
            {
                lista = serviseProveedor.buscarProveedorxNombre(filtro);
            }
            return PartialView("_ListadoProveedorBusqueda", lista);
        }

        //Listas para llenar los combos
        private SelectList listaPais(int idPais = 0)
        {
            IEnumerable<pais> listaPais = servisepais.GetListaPais();
            return new SelectList(listaPais, "id", "nombre", idPais);
        }
    }
}
