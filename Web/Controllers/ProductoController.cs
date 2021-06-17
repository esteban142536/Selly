using Infraestructure.Models;
using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto.Controllers
{
    public class ProductoController : Controller
    {
        IServiseProducto serviseProducto = new ServiseProducto();
        IServiseTipoCategoria ServiseTipoCategoria = new ServiseTipoCategoria();
        

        // GET: Producto

        public ActionResult ingresarProducto(producto produ)
        {
            if (produ==null) {
                return View();
            }
            TipoCategoria tc = new TipoCategoria();
                tc.id = produ.idCategoria;

            ServiseTipoCategoria.asignarCategoria(tc);
            serviseProducto.guardarProducto(produ);
            return View();
        }

        public ActionResult Productos() {
            return View(serviseProducto.listadoProducto());
        }

        public ActionResult MantenimientoProducto(producto produ)
        {
            TipoCategoria tc = new TipoCategoria();
            return View();
        }

        public ActionResult DetalleProducto(int id)
        {
            producto pro = serviseProducto.obtenerProductoID(id);
            return View(pro);
        }

        public ActionResult DetalleProductoCliente()
        {
            return View();
        }


    }
}
