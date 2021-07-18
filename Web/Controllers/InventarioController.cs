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
        IServiseInventario serviseInventa = new ServiseInventario();
        IServiseTipoMovimiento serviseMovi = new ServiseTipoMovimiento();
        IServiseTienda serviseTienda = new ServiseTienda();
        IServiseProveedor serviseProveedor = new ServiseProveedor();
        IServiseEstante serviseEstante = new ServiseEstante();

        [HttpPost]
        public ActionResult Save(inventario inventario, TipoMovimiento tipoMovimiento, String[] proveedor, String[] estante)
        {
            if (tipoMovimiento.tipoEntrada == null)
            {
                tipoMovimiento.tipoEntrada = "No aplica";
            }
            else
            {
                tipoMovimiento.tipoSalida = "No aplica";
            }
            if (inventario == null || tipoMovimiento == null || proveedor == null || estante == null)
            {
                ViewBag.idProveedores = listaproveedor();
                ViewBag.idEstante = listaEstante();
                return View("AgregarInventario", inventario);
            }
            inventario.idTienda = int.Parse(proveedor[0]);

            //primero es asignar el tipo de movimeinto, luego la tienda y luego guardar el inventario
            serviseInventa.crearInventario(inventario);
            return View();
        }

        public ActionResult Index()
        {
            return View(serviseInventa.listadoInventario());
        }

        public ActionResult ReporteEntradaSalida()
        {
            return View(serviseInventa.listadoInventario());
        }

        public ActionResult DetalleInventario(int id)
        {
            return View(serviseInventa.obtenerInventarioID(id));
        }

        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]
        public ActionResult AgregarInventario(string tipodir)
        {
            ViewBag.DetalleCarrito = Carrito.Instancia.Items;
            ViewBag.idProveedores = listaproveedor();
            ViewBag.idEstante = listaEstante();
            return View();
        }

        public ActionResult actualizarCantidad(int idproducto, int cantidad)
        {
            ViewBag.DetalleCarrito = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidad(idproducto, cantidad);
            TempData.Keep();
            ViewBag.idEstante = listaEstante();
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

        //PartialView
        public ActionResult appearEntrada()
        {
            return PartialView("_MovimientoEntrada");
        }
        public ActionResult AppearSalida()
        {
            return PartialView("_MovimientoSalida");
        }

        //Listas
        private SelectList listaTiendas(int idTienda = 0)
        {

            IEnumerable<tienda> listaTiendas = serviseTienda.GetListaTiendas();

            return new SelectList(listaTiendas, "id", "nombre", idTienda);
        }

        private SelectList listaproveedor(int idProveedor = 0)
        {
            IEnumerable<proveedor> listaProveedor = serviseProveedor.listadoProveedor();
            return new SelectList(listaProveedor, "id", "nombreEmpresa", idProveedor);
        }

        private SelectList listaEstante(int idEstante = 0)
        {
            IEnumerable<estante> listaEstante = serviseEstante.GetListaEstante();
            return new SelectList(listaEstante, "id", "nombre", idEstante);
        }
    }
}
