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
    }
}
