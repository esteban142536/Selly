using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryTipoMovimiento
    {
        int agregarTipoMovimiento(TipoMovimiento tm);
        void modificarTipoMovimiento(TipoMovimiento tm);
    }
}
