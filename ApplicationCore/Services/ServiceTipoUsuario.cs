using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public class ServiceTipoUsuario : IServiceTipoUsuario
    {
            IRepositoryTipoUsuario reposi = new RepositoryTipoUsuario();
        public tipoUsuario asignarPermisos(int tu)
        {
            tipoUsuario tius = new tipoUsuario();
            tius.id = tu;
            return reposi.asignarPermisos(tius);
        }

        public IEnumerable<tipoUsuario> listadoPermisos()
        {
            return reposi.listadoPermisos();
        }

        public int obtenerPermisos(int id)
        {
        
            return reposi.obtenerPermisos(id);
        }
    }
}
