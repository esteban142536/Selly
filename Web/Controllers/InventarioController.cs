using ApplicationCore.Services;
using Infraestructure.Models;
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
        IServiseTipoMovimiento serviseMovi = new ServiseTipoMovimiento();
        IServiseTienda serviseTienda = new ServiseTienda();

        public ActionResult EntradaSalida()
        {
            return View(servise.listadoInventario());
        }

      
        public ActionResult DetalleInventario(int id)
        {
            return View(servise.obtenerInventarioID(id));
        }

        private SelectList listaTiendas(int idTienda= 0)
        {

            IEnumerable<tienda> listaTiendas = serviseTienda.GetListaTiendas();
           
            return new SelectList(listaTiendas, "id", "nombre", idTienda);
        }

        public ActionResult CrearInventario()
        {
            ViewBag.idTienda = listaTiendas();
            return View();
        }

        [HttpPost]
        public ActionResult Save(inventario inventario,TipoMovimiento tipoMovimiento, String[] tienda)
        {
            if (inventario == null || tipoMovimiento == null || tienda==null)
            {
                return View();
            }
                inventario.idTienda = int.Parse(tienda[0]);

            //primero es asignar el tipo de movimeinto, luego la tienda y luego guardar el inventario
            // servise.crearInventario(inventario);
            return View();
        }



    }
}
