using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infraestructure.Repository
{
    public class RepositoryInventario : IRepositoryInventario
    {
        IRepositoryEstante repoEsta=new RepositoryEstante();
        IRepositoryProducto repoProdu=new RepositoryProducto();
        IRepositoryProveedor repoProve = new RepositoryProveedor();

        public IEnumerable<inventario> listadoInventario()
        {
            IEnumerable<inventario> lista = null;
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    lista = cdt.inventario.Include(x => x.TipoMovimiento).ToList();
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

        public inventario obtenerInventarioID(int id)
        {

            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    return cdt.inventario.Include(x => x.TipoMovimiento).Include(x => x.usuario).Include(x => x.tienda).Where(x=>x.id==id).FirstOrDefault();
                    
                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }

        public void crearInventario(List<producto> produc, inventario inventa, String[] estante)
        {
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {

                    //salvar primero el inventario, luego los productos
                      cdt.inventario.Add(inventa);
                    cdt.SaveChanges();


                    //llenar estante, producto, inventario y proveedor
                    foreach (var productoItem in produc)
                    {
                        detalleFactura df = new detalleFactura();
                        df.idInventario = inventa.id;
                        df.idProducto = productoItem.id;
                        df.idProveedor = inventa.idTienda;
                        df.idEstante =int.Parse(estante[0]);
                        df.precio = productoItem.costoUnitario;
                        df.cantidadComprada = productoItem.totalStock;
                        /*
                        df.estante= repoEsta.obtenerEstantePorID(int.Parse(estante[0]));
                        df.producto = repoProdu.obtenerProductoID(productoItem.id);
                        df.inventario= inventa;
                        df.proveedor = repoProve.obtenerProveedorID(inventa.idTienda);
                        */
                        
                        cdt.detalleFactura.Add(df);
                    }

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
    }
}
