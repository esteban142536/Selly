using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryPais
    {
        void asignarPais(pais tc);
        pais obtenerCategoriaPorID(int idPais);

        IEnumerable<pais> GetListaPais();
    }
}
