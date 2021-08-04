using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Repository;

namespace ApplicationCore.Services
{
    public class ServiseProducto : IServiseProducto
    {

        IRepositoryProducto repo=new RepositoryProducto();

        public IEnumerable<producto> buscarProductoxNombre(string nombre)
        {
            return repo.buscarProductoxNombre(nombre);
        }

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

        public void actualizarExistDB(int id, int cantUsu, bool esSalida)
        {
            repo.actualizarExistDB(id,cantUsu, esSalida);
        }
        public IEnumerable<string> nombreProductos()
        {
            return repo.listadoProducto().Select(x => x.nombre);
        }
    }
}
