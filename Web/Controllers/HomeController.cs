﻿using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        IServiseInventario serviseInventa = new ServiseInventario();
        IServiseProducto serviseProducto = new ServiseProducto();

        public ActionResult Index()
        {

            IEnumerable<inventario> listadoInventario = serviseInventa.listadoInventario();

            ViewBag.countProducto = serviseProducto.listadoProducto().Count();
            ViewBag.countEntrada = listadoInventario.Where(x => x.TipoMovimiento.tipoSalida==("No aplica")).Count();
            ViewBag.countSalida = listadoInventario.Where(x => x.TipoMovimiento.tipoEntrada==("No aplica")).Count();

            return View(); 
        }

        public ActionResult MenuAdministrador()
        {
            return View();
        }

        public ActionResult AcercaDe()
        {
            return View();
        }
    }
}