using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Util;
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
        IServiseProducto serviseProdu = new ServiseProducto();
        double iva = 0.13;

        [HttpPost]
        public ActionResult Save(inventario inventario, TipoMovimiento tipoMovimiento, String[] proveedor, String[] estante, String[] tienda)
        {

            bool esSalida = false;
            if (tipoMovimiento.tipoEntrada == null)
            {
                tipoMovimiento.tipoEntrada = "No aplica";
                esSalida = true;
            }
            else
            {
                tipoMovimiento.tipoSalida = "No aplica";
            }
            if (inventario == null || tipoMovimiento.tipoEntrada == null || tipoMovimiento.tipoSalida == null)
            {
                ViewBag.DetalleCarrito = Carrito.Instancia.Items;
                ViewBag.idProveedores = listaproveedor();
                ViewBag.idEstante = listaEstante();
                return View("AgregarInventario", inventario);
            }

        int tipoMoviID= serviseMovi.agregarTipoMovimiento(tipoMovimiento);

            List<producto> carritoProducto= new List<producto>();
            foreach (var item in Carrito.Instancia.Items)
            {
                producto pro=new producto();
                pro.id = item.Producto.id;
                pro.nombre = item.Producto.nombre;
                pro.costoUnitario = item.Producto.costoUnitario;
                pro.idCategoria = item.Producto.idCategoria;
                pro.totalStock = item.totalStock;
                pro.descripcion = item.Producto.descripcion;
                pro.imagen = item.Producto.imagen;

                carritoProducto.Add(pro);

                serviseProdu.actualizarExistDB(item.id,item.totalStock, esSalida);
            }
            usuario user = (usuario)Session["Usuario"];
            inventario.idUsuario = user.id;
            inventario.fecha = DateTime.Now.ToString();
            inventario.idTienda = int.Parse(tienda[0]);
            inventario.idTipoMovimiento = tipoMoviID;
            inventario.TipoMovimiento = tipoMovimiento;
            inventario.totalPagado = Carrito.Instancia.GetTotal();
            inventario.iva = iva;


            serviseInventa.crearInventario(carritoProducto, inventario, estante, proveedor);


            Carrito.Instancia.eliminarCarrito();
            ViewBag.DetalleCarrito = Carrito.Instancia.Items;
            ViewBag.idProveedores = listaproveedor();
            ViewBag.idTienda = listaTiendas();
            ViewBag.idEstante = listaEstante();

            //muestra las notificaciones
            TempData["NotificationMessage"] = Web.Util.SweetAlertHelper.Mensaje("Inventario", "Productos agregado al inventario", SweetAlertMessageType.success);
            return RedirectToAction("AgregarInventario");
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
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }

            ViewBag.DetalleCarrito = Carrito.Instancia.Items;
            ViewBag.idProveedores = listaproveedor();
            ViewBag.idTienda = listaTiendas();
            ViewBag.idEstante = listaEstante();
            return View();
        }

        public ActionResult actualizarCantidad(int idproducto, int cantidad)
        {
            ViewBag.DetalleCarrito = Carrito.Instancia.Items;
            Carrito.Instancia.SetItemCantidad(idproducto, cantidad);
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
