using Infraestructure.Models;
using System.Linq;
using System;

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

        public int obtenerPermisos(int id)
        {
            int permiso = 0;
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;
                permiso = cdt.tipoUsuario.Where(x=>x.id==id).FirstOrDefault<tipoUsuario>().permisoUsuario;

            }
            return permiso;
            }
    }
}
