using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryProveedor
    {
        IEnumerable<proveedor> listadoProveedor();

        proveedor obtenerProveedorID(int id);
    }
}
