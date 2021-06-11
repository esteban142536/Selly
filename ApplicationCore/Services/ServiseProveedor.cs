using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{

    public class ServiseProveedor : IServiseProveedor
    {
        IRepositoryProveedor repo = new RepositoryProveedor();

        public IEnumerable<proveedor> listadoProveedor()
        {
          return repo.listadoProveedor();
        }

        public proveedor obtenerProveedorID(int id)
        {
            return repo.obtenerProveedorID(id);
        }
    }
}
