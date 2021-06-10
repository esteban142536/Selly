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
        public void asignarCategoria(TipoCategoria tc)
        {
            IRepositoryTipoCategoria reposi = new RepositoryTipoCategoria();
            reposi.asignarCategoria(tc);
        }

        public TipoCategoria obtenerCategoriaPorID(int idProducto)
        {
            throw new NotImplementedException();
        }
    }
}
