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

                ViewBag.idTienda = listaTiendas();
                ViewBag.DetalleCarrito = Carrito.Instancia.Items;
                ViewBag.idProveedores = listaproveedor();
                ViewBag.idEstante = listaEstante();
                TempData["NotificationMessage"] = Web.Util.SweetAlertHelper.Mensaje("Inventario", "Hay campos que estan vacios", SweetAlertMessageType.error);
                return View("AgregarInventario", inventario);
            }


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

                bool rest = serviseProdu.actualizarExistDB(item.id, item.totalStock, esSalida);

                if (!rest)
                {
                    ViewBag.DetalleCarrito = Carrito.Instancia.Items;
                    ViewBag.idProveedores = listaproveedor();
                    ViewBag.idTienda = listaTiendas();
                    ViewBag.idEstante = listaEstante();
                    TempData["NotificationMessage"] = Web.Util.SweetAlertHelper.Mensaje("Inventario", "La cantidad ingresada excede el limite máximo", SweetAlertMessageType.warning);
                    return View("AgregarInventario", inventario);

                }
            }

        int tipoMoviID= serviseMovi.agregarTipoMovimiento(tipoMovimiento);

            usuario user = (usuario)Session["Usuario"];
            inventario.idUsuario = user.id;
            inventario.fecha = DateTime.Now.ToString("dd/MM/yyyy");
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



        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]
        public ActionResult Index()
        {
            usuario user = (usuario)Session["Usuario"];
            if (user.id != 1) { 
            return View(serviseInventa.listadoInventario().Where(x=>x.idUsuario==user.id).ToList());
            
            }
            return View(serviseInventa.listadoInventario());

        }

        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]

        public ActionResult ReporteEntradaSalida()
        {
            usuario user = (usuario)Session["Usuario"];
            if (user.id != 1)
            {
                return View(serviseInventa.listadoInventario().Where(x => x.idUsuario == user.id).ToList());

            }
            return View(serviseInventa.listadoInventario());
        }




        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]

        public ActionResult DetalleInventario(int id)
        {
            return View(serviseInventa.obtenerInventarioID(id));
        }




        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]

        public ActionResult AgregarInventario(string tipodir)
        {
            List<estante> produEsta = new List<estante>();
            estante esta = null;

            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }

            ViewBag.DetalleCarrito = Carrito.Instancia.Items;
            ViewBag.idProveedores = listaproveedor();
            ViewBag.idTienda = listaTiendas();

            foreach (var producto in Carrito.Instancia.Items)
            {
                foreach (var estante in producto.Producto.productoEstante)
                {
                    esta = serviseEstante.obtenerEstantePorID(estante.idEstante);

                    if (!produEsta.Where(x => x.id == esta.id).Any())
                    {
                        produEsta.Add(serviseEstante.obtenerEstantePorID(estante.idEstante));
                    }


                }
            }

            ViewBag.idEstante = listaEstantePersonal(produEsta);

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

        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]
        public ActionResult buscarInventarioxFecha(DateTime fechaInicio, DateTime fechaFinal)
        {
            IEnumerable<inventario> lista = null;

            if (string.IsNullOrEmpty(fechaInicio.ToString())||string.IsNullOrEmpty(fechaFinal.ToString()))
            {

                lista = serviseInventa.listadoInventario();
                TempData["NotificationMessage"] = Web.Util.SweetAlertHelper.Mensaje("Inventario", "Una de las fechas esta vacia", SweetAlertMessageType.error);
            }
            else
            {
                lista = serviseInventa.listadoInventario().Where(x => DateTime.Parse(x.fecha).CompareTo(fechaFinal) == -1 &&
                DateTime.Parse(x.fecha).CompareTo(fechaInicio) == 1 ).ToList();
            }
            return PartialView("_InformeInventario", lista);
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

        private SelectList listaEstantePersonal(List<estante> idEstante)
        {
            return new SelectList(idEstante, "id", "nombre");
        }
    }
}
