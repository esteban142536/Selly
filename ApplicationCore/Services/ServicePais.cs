using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePais : IServicePais
    {
        IRepositoryPais repository = new RepositoryPais();

        public void asignarPais(pais tc)
        {
            repository.asignarPais(tc);
        }

        public IEnumerable<pais> GetListaPais()
        {
            return repository.GetListaPais();
        }

        public pais obtenerPaisPorID(int idPais)
        {
            return repository.obtenerPaisPorID(idPais);
        }
    }
}
