using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiseTipoCategoria : IServiseTipoCategoria
    {
            IRepositoryTipoCategoria reposi = new RepositoryTipoCategoria();
        public void asignarCategoria(TipoCategoria tc)
        {
            reposi.asignarCategoria(tc);
        }

        public IEnumerable<TipoCategoria> GetListaTipoCategoria()
        {
            return reposi.GetListaTipoCategoria();
        }

        public TipoCategoria obtenerCategoriaPorID(int idProducto)
        {
            throw new NotImplementedException();
        }
    }
}
