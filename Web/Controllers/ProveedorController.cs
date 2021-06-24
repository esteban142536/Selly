using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto.Controllers
{
    public class ProveedorController : Controller
    {
        IServiseProveedor serviseProveedor = new ServiseProveedor();
        IServicePais servisepais = new ServicePais();

        public ActionResult Proveedores()
        {
            ViewBag.Title = "Listado de provedores";
            return View(serviseProveedor.listadoProveedor());
        }

 
        public ActionResult DetalleProveedor(int id)
        {
            ViewBag.Title = "Datos del proveedor";
            return View(serviseProveedor.obtenerProveedorID(id));
        }

        public ActionResult MantenimientoProveedor()
        {
            ViewBag.idCategoria = listaPais();
            return View();
        }

        //Combo de pais
        private SelectList listaPais(int idPais = 0)
        {

            IEnumerable<pais> listaPais = servisepais.GetListaPais();

            return new SelectList(listaPais, "id", "pais", idPais);
        }
    }
}
