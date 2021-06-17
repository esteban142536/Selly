using Infraestructure.Models;
using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace proyecto.Controllers
{
    public class ProductoController : Controller
    {
        IServiseProducto serviseProducto = new ServiseProducto();
        IServiseTipoCategoria ServiseTipoCategoria = new ServiseTipoCategoria();
        IServiseProveedor serviseProveedor = new ServiseProveedor();
        IServiseEstante serviseEstante = new ServiseEstante();
        MemoryStream target = new MemoryStream();


        // GET: Producto



        public ActionResult Productos() {
            return View(serviseProducto.listadoProducto());
        }


        //para guardar productos nuevos
        [HttpPost]
        public ActionResult Save(producto produ, String[] categoria, String[] proveedor, String[] estante, HttpPostedFileBase ImageFile)
        {
            //valida si uno de sus datos estab vacions
            if (!ModelState.IsValid)
            {
                return View("MantenimientoProducto",produ);
            }

            //valida si existe una imagen
            if (produ.imagen == null)
            {
                if (ImageFile != null)
                {
                    ImageFile.InputStream.CopyTo(target);
                    produ.imagen = target.ToArray();
                    ModelState.Remove("Imagen");
                }

            }



            produ.idCategoria = int.Parse(categoria[0]);

            serviseProducto.guardarProducto(produ, int.Parse(proveedor[0]), int.Parse(estante[0]));

            return RedirectToAction("MantenimientoProducto");
        }



        public ActionResult MantenimientoProducto()
        {
            ViewBag.idCategoria = listaTipoCategoria();
            ViewBag.idProveedores = listaProveedores();
            ViewBag.idEstantes = listaEstantes();
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

        //listas para llenar los Combos
        private SelectList listaTipoCategoria(int idCategoria = 0)
        {

            IEnumerable<TipoCategoria> listaTiendas = ServiseTipoCategoria.GetListaTipoCategoria();

            return new SelectList(listaTiendas, "id", "Descripcion", idCategoria);
        }
        private SelectList listaProveedores(int idProveedor = 0)
        {

            IEnumerable<proveedor> listaProveedores = serviseProveedor.listadoProveedor();

            return new SelectList(listaProveedores, "id", "nombreEmpresa", idProveedor);
        }
        private SelectList listaEstantes(int idEstante = 0)
        {

            IEnumerable<estante> listaEstante = serviseEstante.GetListaEstante();

            return new SelectList(listaEstante, "id", "nombre", idEstante);
        }

    }
}
