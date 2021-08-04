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

        public IEnumerable<proveedor> buscarProveedorxNombre(string nombre)
        {
            return repo.buscarProveedorxNombre(nombre);
        }

        public void guardarProveedor(proveedor proveedor)
        {
            repo.guardarProveedor(proveedor);
        }

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
