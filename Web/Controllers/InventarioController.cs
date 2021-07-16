using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.ViewModel;

namespace proyecto.Controllers
{
    public class InventarioController : Controller
    {
        IServiseInventario servise = new ServiseInventario();
        IServiseTipoMovimiento serviseMovi = new ServiseTipoMovimiento();
        IServiseTienda serviseTienda = new ServiseTienda();
        IServiseProveedor serviseProveedor = new ServiseProveedor();

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

       // [CustomAuthorize((int)TipoUsuario.Administrador)]
        public ActionResult AgregarInventario(string tipodir)
        {
            ViewBag.DetalleCarrito = Carrito.Instancia.Items;
            ViewBag.idProveedores = listaproveedor(null);
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

        public ActionResult actualizarCantidad(int idproducto, int cantidad)
        {
            ViewBag.DetalleCarrito = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidad(idproducto, cantidad);
            TempData.Keep();
            return PartialView("_ListadoInventario", Carrito.Instancia.Items);

        }
        public ActionResult actualizarOrdenCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantidadLibros = Carrito.Instancia.Items.Count();
            return PartialView("_cantidadCarrito");

        }


        private MultiSelectList listaproveedor(ICollection<proveedor> proveedores)
        {
            IEnumerable<proveedor> listaProveedores = serviseProveedor.listadoProveedor();
            int[] listaEstantesSelect = null;

            if (proveedores != null)
            {

                listaEstantesSelect = proveedores.Select(c => c.id).ToArray();
            }

            return new MultiSelectList(listaProveedores, "id", "nombreEmpresa", listaEstantesSelect);

        }

     /*   private SelectList listaEntradas(int? id) {
            IServiceAutor _ServiceAutor = new ServiceAutor();
            IEnumerable<Autor> listaAutores = _ServiceAutor.GetAutor();
            //Autor SelectAutor = listaAutores.Where(c => c.IdAutor == idAutor).FirstOrDefault();
            return new SelectList(listaAutores, "IdAutor", "Nombre", idAutor);
        }
     */

    }
}
