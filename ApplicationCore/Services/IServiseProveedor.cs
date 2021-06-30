using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiseProveedor
    {
        void guardarProveedor(proveedor proveedor);
        IEnumerable<proveedor> listadoProveedor();
        proveedor obtenerProveedorID(int id);
    }
}
