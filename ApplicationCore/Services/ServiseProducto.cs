using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Models;
using Infraestructure.Repository;

namespace ApplicationCore.Services
{
    public class ServiseProducto : IServiseProducto
    {

        IRepositoryProducto repo=new RepositoryProducto();

        public void guardarProducto(producto producto, String[] idProveedor, String[] idEstante)
        {
            repo.guardarProducto(producto, idProveedor, idEstante);
        }

        public IEnumerable<producto> listadoProducto()
        {
            return repo.listadoProducto();
        }

        public producto obtenerProductoID(int id)
        {
            return repo.obtenerProductoID(id);
        }
    }
}
