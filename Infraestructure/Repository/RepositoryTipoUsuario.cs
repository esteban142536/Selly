using Infraestructure.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Infraestructure.Repository
{
    public class RepositoryTipoUsuario : IRepositoryTipoUsuario
    {

        public tipoUsuario asignarPermisos(tipoUsuario tu)
        {
            using (contextData cdt = new contextData())
            {

                cdt.Configuration.LazyLoadingEnabled = false;
                cdt.tipoUsuario.Add(tu);
                cdt.SaveChanges();

            }
            return tu;
        }

        public IEnumerable<tipoUsuario> listadoPermisos()
        {
            try {
                using (contextData cdt = new contextData())
                {

                    return cdt.tipoUsuario.ToList();

                }
                }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public int obtenerPermisos(int id)
        {
            int permiso = 0;
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;
                permiso = cdt.tipoUsuario.Where(x=>x.id==id).FirstOrDefault<tipoUsuario>().id;

            }
            return permiso;
            }
    }
}
