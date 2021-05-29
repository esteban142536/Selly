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
        public tipoUsuario asignarPermisos(int tu)
        {
            IRepositoryTipoUsuario reposi = new RepositoryTipoUsuario();
            tipoUsuario tius = new tipoUsuario();
            tius.tipoUsuario1 = tu.ToString();
            return reposi.asignarPermisos(tius);
        }
    }
}
