using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryTipoUsuario
    {
        tipoUsuario asignarPermisos(tipoUsuario tu);
        int obtenerPermisos(int id);
    }
}
