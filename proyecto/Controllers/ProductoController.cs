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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ingresarProducto(producto produ, String categoria)
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

    }
}
