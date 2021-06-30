using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{

    public class ServiseContactos : IServiseContactos
    {
        IRepositoryContactos repository = new RepositoryContactos();

        public void GuardarContactos(contactos contactos)
        {
            repository.GuardarContactos(contactos);
        }

        public IEnumerable<contactos> listadoContactos()
        {
            return repository.listadoContactos();
        }

        public contactos obtenerContactoID(int id)
        {
            return repository.obtenerContactoID(id);
        }
    }
}
