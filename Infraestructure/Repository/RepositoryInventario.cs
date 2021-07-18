using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryInventario : IRepositoryInventario
    {
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

        public void crearInventario(inventario inve)
        {
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    cdt.inventario.Add(inve);
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
