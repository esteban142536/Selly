using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infraestructure.Repository
{
    public class RepositoryProducto : IRepositoryProducto
    {

        IRepositoryProveedor repoPro = new RepositoryProveedor();
        IRepositoryEstante repoEsta = new RepositoryEstante();
        private int count;

        public void guardarProducto(producto producto, String[] idProveedor, String[] idEstante)
        {
            proveedor pro;
            producto produExist = obtenerProductoID(producto.id);

            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    if (produExist == null)
                    {
                        //si producto no existe, es nuevo
                        //carga los proveedores a la tabla intermedia 
                        foreach (var idPro in idProveedor) { 
                        pro = repoPro.obtenerProveedorID(int.Parse(idPro));
                        cdt.proveedor.Attach(pro);
                        producto.proveedor.Add(pro);
                        }

                        //salva el producto 
                        cdt.producto.Add(producto);

                        //carga la tabla intermedia de ubicacion 
                        foreach (var idEsta in idEstante)
                        {
                            productoEstante pe = new productoEstante();
                            pe.idProducto = producto.id;
                            pe.idEstante = int.Parse(idEsta);
                            pe.cantidad = producto.totalStock;
                            producto.productoEstante.Add(pe);
                        }


                        cdt.SaveChanges();
                    }
                    else
                    {
                        //actualiza el producto 
                        cdt.producto.Add(producto);
                        cdt.Entry(producto).State = EntityState.Modified;


                        //actualiza los proveedores a la tabla intermedia 
                        var proveedoresLista = new HashSet<string>(idProveedor);
                        cdt.Entry(producto).Collection(p => p.proveedor).Load();
                        var nuevoProveedorLista = cdt.proveedor.Where(x => proveedoresLista.Contains(x.id.ToString())).Include(x => x.pais).Include(x => x.contactos).Include(x => x.detalleFactura).ToList();
                        producto.proveedor = nuevoProveedorLista;
                        cdt.Entry(producto).State = EntityState.Modified;


                        //actualizar tabla intermedia de ubicacion usando muchos a muchos
                        //idEstante arreglo de los identificadores de las ubicaciones o estantes
                        if (idEstante != null)
                        {
                            //Obtener los estantes registrados del producto a modificar
                            List<productoEstante> estantesdelProducto = cdt.productoEstante.Where(x=>x.idProducto==producto.id).ToList();
                            // Borrar los estantes existentes del producto
                            foreach (var item in estantesdelProducto)
                            {                             
                                producto.productoEstante.Remove(item);
                            }
                            //Registrar los estantes especificados
                            foreach (var estante in idEstante)
                            {
                                productoEstante pe = new productoEstante();
                                pe.idProducto = producto.id;
                                pe.idEstante = int.Parse(estante);
                                pe.cantidad = 0;
                                cdt.productoEstante.Add(pe);
                            }
                        }
                        cdt.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    string mensaje = "";
                    Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }

        }

        public IEnumerable<producto> listadoProducto()
        {
            IEnumerable<producto> lista=null;

            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {

                    lista = cdt.producto.Include(x=>x.TipoCategoria).Where(x => x.totalStock > 0).ToList();
                    return lista;

                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }

        public producto obtenerProductoID(int id)
        {
            producto producto = null;
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;
                producto = cdt.producto.Include(x => x.TipoCategoria).Include(x=>x.proveedor).Include(x => x.productoEstante).Include("productoEstante.estante")
                    .Where(x => x.id == id).FirstOrDefault();
            }
            return producto;
            }


        public bool actualizarExistDB(int id, int cantUsu, bool esSalida) {
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;
                try
                {
                    producto oldProd = obtenerProductoID(id);
                    oldProd.TipoCategoria = null;
                    oldProd.productoEstante = null;
                    oldProd.proveedor = null;

                    if (esSalida)
                    {
                        oldProd.totalStock -= cantUsu;
                        if (oldProd.totalStock < oldProd.cantMinima)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        oldProd.totalStock += cantUsu;
                        if (oldProd.totalStock > oldProd.cantMaxima)
                        {
                            return false;
                        }
                    }

                    cdt.producto.Add(oldProd);
                    cdt.Entry(oldProd).State = EntityState.Modified;
                    cdt.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }

        public IEnumerable<producto> buscarProductoxNombre(string nombre) {
            IEnumerable<producto> lista = null;
            using (contextData ctx = new contextData())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.producto.ToList().
                    FindAll(l => l.nombre.ToLower().Contains(nombre.ToLower()));
            }
            return lista;
        }

        public List<IGrouping<int, detalleFactura>> listadoProductoMayorSalidas()
        {
            List<IGrouping<int,detalleFactura>> lista = null;

            try {
                using (contextData ctx = new contextData())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                  lista = ctx.detalleFactura.GroupBy(x => x.idProducto).OrderByDescending(x=>x.Count()).Take(3).ToList();
                }
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
            return lista;
        }

        public IEnumerable<producto> listadoProductoReponer()
        {
            IEnumerable<producto> lista = null;

            try
            {
                using (contextData cdt = new contextData())
                {
                    cdt.Configuration.LazyLoadingEnabled = false;

                    lista = cdt.producto.Include(x => x.TipoCategoria).OrderBy(x => x.cantMinima).Take(6).ToList();
                }
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
            return lista;
        }
    }
}
