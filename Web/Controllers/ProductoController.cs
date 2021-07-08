using ApplicationCore.Services;
using Infraestructure;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace proyecto.Controllers
{
    public class ProductoController : Controller
    {
        IServiseProducto serviseProducto = new ServiseProducto();
        IServiseTipoCategoria ServiseTipoCategoria = new ServiseTipoCategoria();
        IServiseProveedor serviseProveedor = new ServiseProveedor();
        IServiseEstante serviseEstante = new ServiseEstante();
        MemoryStream target = new MemoryStream();


        //Guarda productos nuevos
        [HttpPost]
        public ActionResult Save(producto produ, String[] categoria, String[] proveedor, String[] estante, HttpPostedFileBase ImageFile)
        {
            produ.idCategoria = int.Parse(categoria[0]);

            //Valida si existe una imagen
            if (produ.imagen == null)
            {
                if (ImageFile != null)
                {
                    ImageFile.InputStream.CopyTo(target);
                    produ.imagen = target.ToArray();
                    ModelState.Remove("Imagen");
                }
            }
               if (produ.nombre==null||produ.descripcion == null||produ.costoUnitario == 0||produ.totalStock == 0||produ.cantMaxima == 0||produ.cantMinima==0)
            {
                ViewBag.idCategoria = listaTipoCategoria(produ.idCategoria);
                ViewBag.idProveedores = listaproveedor(null);
                ViewBag.idEstantes = listaEstantes(null);
                return View("AgregarProducto", produ);
            }
          
            serviseProducto.guardarProducto(produ, proveedor, estante);
            return RedirectToAction("Index");
        }

        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]
        public ActionResult AgregarProducto()
        {
            ViewBag.idCategoria = listaTipoCategoria();
            ViewBag.idProveedores = listaproveedor(null);
            ViewBag.idEstantes = listaEstantes(null);
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

        //Para editar productos
     //   [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]
        public ActionResult EditarProducto(int? id)
        {
        try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                producto pro = serviseProducto.obtenerProductoID(id.Value);

                if (pro == null)
                {
                    TempData["Message"] = "No existe el producto solicitado";
                    return RedirectToAction("Index");
                }
                
                ViewBag.idCategoria = listaTipoCategoria(pro.idCategoria);
                ViewBag.idProveedores = listaproveedor(null);
                ViewBag.idEstantes = listaEstantes(null);
                return View(pro);
            }
        catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Index()
        {
            return View(serviseProducto.listadoProducto());
        }

        public ActionResult ReporteProducto()
        {
            return View(serviseProducto.listadoProducto());
        }

        //Listas para llenar los combos
        private SelectList listaTipoCategoria(int idCategoria = 0)
        {

            IEnumerable<TipoCategoria> listaTipoCategorias = ServiseTipoCategoria.GetListaTipoCategoria();

            return new SelectList(listaTipoCategorias, "id", "Descripcion", idCategoria);
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
        private MultiSelectList listaEstantes(ICollection<proveedor> proveedores)
        {
            IEnumerable<estante> listaEstante = serviseEstante.GetListaEstante();
            int[] listaEstanteSelect = null;

            if (proveedores != null)
            {

                listaEstanteSelect = proveedores.Select(c => c.id).ToArray();
            }

            return new MultiSelectList(listaEstante, "id", "nombre", listaEstanteSelect);

        }

    }
}
