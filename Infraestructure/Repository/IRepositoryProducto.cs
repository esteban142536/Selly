using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryProducto
    {
        void guardarProducto(producto producto,int idProveedor, int idEstante);

        IEnumerable<producto> listadoProducto();

        producto obtenerProductoID(int id);
    }
}
