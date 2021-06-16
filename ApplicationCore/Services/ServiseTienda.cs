using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiseTienda : IServiseTienda
    {
        IRepositoryTienda rt = new RepositoryTienda();
        public int agregarTienda(tienda td)
        {
            return rt.agregarTienda(td);
        }

        public IEnumerable<tienda> GetListaTiendas()
        {
            return rt.GetListaTiendas();
        }
    }
}
