using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryEstante : IRepositoryEstante
    {
        public IEnumerable<estante> GetListaEstante()
        {
            IEnumerable<estante> lista = null;
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    lista = cdt.estante.ToList();
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


        public estante obtenerEstantePorID(int idEstante)
        {
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    return cdt.estante.Where(x => x.id == idEstante).FirstOrDefault();

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
