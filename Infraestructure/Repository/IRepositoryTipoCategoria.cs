using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryTipoCategoria
    {
        void asignarCategoria(TipoCategoria tc);
        TipoCategoria obtenerCategoriaPorID(int idProducto);
    }
}
