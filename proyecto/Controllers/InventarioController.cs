using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto.Controllers
{
    public class InventarioController : Controller
    {
        IServiseInventario servise = new ServiseInventario();

        public ActionResult Index()
        {
            return View(servise.listadoInventario());
        }

      
        public ActionResult Details(int id)
        {
            return View(servise.obtenerInventarioID(id));
        }

    }
}
