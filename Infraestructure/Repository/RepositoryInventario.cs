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
                    return cdt.inventario.Include(x => x.TipoMovimiento).Include(x => x.usuario).Include(x => x.tienda).Include("detalleFactura.Producto").Include("detalleFactura.Producto.TipoCategoria").Where(x=>x.id==id).FirstOrDefault();
                    
                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }

        public void crearInventario(List<producto> produc, inventario inventa, String[] estante, String[] prove)
        {
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {

                    //salvar primero el inventario, luego los productos
                      cdt.inventario.Add(inventa);

                    //llenar estante, producto, inventario y proveedor
                    foreach (var productoItem in produc)
                    {
                        detalleFactura df = new detalleFactura();
                        df.idInventario = inventa.id;
                        df.idProducto = productoItem.id;
                        df.idProveedor = int.Parse(prove[0]);
                        df.idEstante =int.Parse(estante[0]);
                        df.precio = productoItem.costoUnitario;
                        df.cantidadComprada = productoItem.totalStock;
                        
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
