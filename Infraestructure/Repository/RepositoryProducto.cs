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

        public void guardarProducto(producto producto, int idProveedor, int idEstante)
        {
           proveedor pro;

            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    //carga los proveedores a la tabla intermedia
                    pro = repoPro.obtenerProveedorID(idProveedor);
                    cdt.proveedor.Attach(pro);
                    producto.proveedor.Add(pro);

                    //salva el producto
                    cdt.producto.Add(producto);

                    //carga la tabla intermedia de ubicacion
                    productoEstante pe = new productoEstante();
                    pe.idProducto = producto.id;
                    pe.idEstante = idEstante;
                    pe.cantidad = producto.totalStock;
                    cdt.productoEstante.Add(pe);

                    cdt.SaveChanges();
                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
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

                    lista = cdt.producto.Include(x=>x.TipoCategoria).ToList();
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
    }
}
