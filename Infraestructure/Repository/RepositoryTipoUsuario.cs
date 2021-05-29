using Infraestructure.Models;

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
    }
}
