using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiceTipoUsuario
    {
        tipoUsuario asignarPermisos(int tu);
        int obtenerPermisos(int id);
        IEnumerable<tipoUsuario> listadoPermisos();
    }
}
