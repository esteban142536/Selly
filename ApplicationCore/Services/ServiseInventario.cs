using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiseInventario : IServiseInventario
    {
        IRepositoryInventario repo = new RepositoryInventario();

        public void crearInventario(List<producto> inve, inventario inventa, String[] estante, String[] Proveedor)
        {
            repo.crearInventario(inve, inventa, estante, Proveedor);
        }

        public IEnumerable<inventario> listadoInventario()
        {
            return repo.listadoInventario();
        }

        public inventario obtenerInventarioID(int id)
        {
            return repo.obtenerInventarioID(id);
        }
    }
}
