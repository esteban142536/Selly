using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryTipoMovimiento : IRepositoryTipoMovimiento
    {
        public int agregarTipoMovimiento(TipoMovimiento tm)
        {
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    cdt.TipoMovimiento.Add(tm);
                    cdt.SaveChanges();
                    return tm.id;
                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }

        public void modificarTipoMovimiento(TipoMovimiento tm)
        {
            throw new NotImplementedException();
        }
    }
}
