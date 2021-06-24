using ApplicationCore.Services;
using Infraestructure;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
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


        // GET: Producto

        //listas para llenar los Combos
        private SelectList listaTipoCategoria(int idCategoria = 0)
        {

            IEnumerable<TipoCategoria> listaTipoCategorias = ServiseTipoCategoria.GetListaTipoCategoria();

            return new SelectList(listaTipoCategorias, "id", "Descripcion", idCategoria);
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


        public ActionResult Index() {
            return View(serviseProducto.listadoProducto());
        }


        //para guardar productos nuevos
        [HttpPost]
        public ActionResult Save(producto produ, String[] categoria, String[] proveedor, String[] estante, HttpPostedFileBase ImageFile)
        {

        
            produ.idCategoria = int.Parse(categoria[0]);
                produ.TipoCategoria = ServiseTipoCategoria.obtenerCategoriaPorID(int.Parse(categoria[0]));


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
            //la validacion esta fallando, revisar posteriormente
         /*   if (!ModelState.IsValid)
            {
                MantenimientoProducto();
                return View("MantenimientoProducto");
            }
            */

            serviseProducto.guardarProducto(produ, int.Parse(proveedor[0]), int.Parse(estante[0]));
            return RedirectToAction("Index");

        }


        [CustomAuthorize((int)TipoUsuario.Administrador, (int)TipoUsuario.Empleado)]
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

        //para editar productos antiguos
public ActionResult Edit(int? id)
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
            TempData["Message"] = "No existe el libro solicitado";
            return RedirectToAction("Index");
        }
                
                ViewBag.idCategoria = listaTipoCategoria(pro.idCategoria);
                ViewBag.idProveedores = listaProveedores();
                ViewBag.idEstantes = listaEstantes();
                return View(pro);
    }
    catch (Exception ex)
    {

        Log.Error(ex, MethodBase.GetCurrentMethod());
        TempData["Message"] = "Error al procesar los datos! " + ex.Message;
        return RedirectToAction("Index");
    }
}
        


    }
}
