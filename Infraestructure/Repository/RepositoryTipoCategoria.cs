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

        public TipoCategoria obtenerCategoriaPorID(int idProducto)
        {
            //cdt.Configuration.LazyLoadingEnabled = false;
            throw new NotImplementedException();
        }
    }
}
