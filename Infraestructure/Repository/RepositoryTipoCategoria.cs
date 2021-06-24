using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryTipoCategoria : IRepositoryTipoCategoria
    {
        public void asignarCategoria(TipoCategoria tc)
        {
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    cdt.TipoCategoria.Add(tc);
                    cdt.SaveChanges();
                }
                catch (Exception e)
                {
                    string mensaje = "";
                    Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }

        public IEnumerable<TipoCategoria> GetListaTipoCategoria()
        {
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                  return  cdt.TipoCategoria.ToList();
                   
                }
                catch (Exception e)
                {
                    string mensaje = "";
                    Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }

        public TipoCategoria obtenerCategoriaPorID(int idProducto)
        {
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    return cdt.TipoCategoria.Where(x=>x.id==idProducto).FirstOrDefault();

                }
                catch (Exception e)
                {
                    string mensaje = "";
                    Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }
    }
}
