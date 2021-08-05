using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationCore.Services
{
   public interface IServiseInventario
    {
        IEnumerable<inventario> listadoInventario();
        inventario obtenerInventarioID(int id);
        void crearInventario(List<producto> inve, inventario inventa, String[] estante, String[] prove);
    }
}
