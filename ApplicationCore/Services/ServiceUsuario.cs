using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public class ServiceUsuario : IServiceUsuario
    {
      
        public usuario logIn(string email, string clave)
        {
            IRepositoryUsuario reposi = new RepositoryUsuario();
            return reposi.logIn(email,clave);
        }

        public void SignIn(usuario usuario)
        {
            IRepositoryUsuario reposi = new RepositoryUsuario();
            reposi.SignIn(usuario);
        }
    }
}
