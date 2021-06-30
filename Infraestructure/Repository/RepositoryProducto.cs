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
                        //si producto no existe 
                        //carga los proveedores a la tabla intermedia 
                        pro = repoPro.obtenerProveedorID(int.Parse(idProveedor[0]));
                        cdt.proveedor.Attach(pro);
                        producto.proveedor.Add(pro);

                        //salva el producto 
                        cdt.producto.Add(producto);

                        //carga la tabla intermedia de ubicacion 
                        productoEstante pe = new productoEstante();
                        pe.idProducto = producto.id;
                        pe.idEstante = int.Parse(idEstante[0]);
                        pe.cantidad = producto.totalStock;
                        cdt.productoEstante.Add(pe);

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

                        //actualiza la tabla intermedia de ubicacion 

                        cdt.Entry(producto).Collection(p => p.productoEstante).Load();

                        foreach (productoEstante podues in producto.productoEstante)
                        {
                            cdt.productoEstante.Remove(podues);//--> esto esta mal, preguntar a la profe una alternativa 
                            break;
                        }
                        productoEstante pe = new productoEstante();
                        pe.idProducto = producto.id;
                        pe.idEstante = int.Parse(idEstante[0]);
                        pe.cantidad = producto.totalStock;
                        cdt.productoEstante.Add(pe);
                        cdt.Entry(producto).State = EntityState.Modified;

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
