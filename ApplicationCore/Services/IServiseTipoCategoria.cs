using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiseTipoCategoria
    {
        void asignarCategoria(TipoCategoria tc);
        TipoCategoria obtenerCategoriaPorID(int idProducto);
        IEnumerable<TipoCategoria> GetListaTipoCategoria();
    }
}
