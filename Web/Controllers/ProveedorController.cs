using ApplicationCore.Services;
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


    }
}
