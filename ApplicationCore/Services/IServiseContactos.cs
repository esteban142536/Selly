using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiseContactos
    {
        contactos obtenerContactoID(int id);
        IEnumerable<contactos> listadoContactos();

        void GuardarContactos(contactos contactos);
    }
}
