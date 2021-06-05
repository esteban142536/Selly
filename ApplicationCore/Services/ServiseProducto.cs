using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Models;

namespace ApplicationCore.Services
{
    public class ServiseProducto : IServiseProducto
    {
        public void guardarProducto(producto producto)
        {
            IServiseProducto serviseProducto = new ServiseProducto();
            serviseProducto.guardarProducto(producto);
        }
    }
}
