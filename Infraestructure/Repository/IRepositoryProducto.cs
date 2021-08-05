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
        void guardarProducto(producto producto, String[] idProveedor, String[] idEstante);

        IEnumerable<producto> listadoProducto();

        producto obtenerProductoID(int id);
        void actualizarExistDB(int id, int cantUsu, bool esSalida);
        IEnumerable<producto> buscarProductoxNombre(string nombre);
        IEnumerable<producto> listadoProductoMayorSalidas();
    }
}
