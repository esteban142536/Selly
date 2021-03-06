using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiseProducto
    {
        void guardarProducto(producto producto, String[] idProveedor, String[] idEstante);
        IEnumerable<producto> listadoProducto();
        producto obtenerProductoID(int id);
        bool actualizarExistDB(int id, int cantUsu, bool esSalida);
        IEnumerable<producto> buscarProductoxNombre(string nombre);
        IEnumerable<string> nombreProductos();
        List<IGrouping<int, detalleFactura>> listadoProductoMayorSalidas();
        IEnumerable <producto> listadoProductoReponer();
    }
}
