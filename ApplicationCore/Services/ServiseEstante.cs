using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiseEstante : IServiseEstante
    {
        IRepositoryEstante repositoryEstante = new RepositoryEstante();
        public IEnumerable<estante> GetListaEstante()
        {
            return repositoryEstante.GetListaEstante();
        }
    }
}
