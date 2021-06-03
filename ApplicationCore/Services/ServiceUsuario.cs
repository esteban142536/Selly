using ApplicationCore.Utils;
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
            string crytclave = Cryptography.EncrypthAES(clave);
            return reposi.logIn(email, crytclave);
        }

        public void SignIn(usuario usuario)
        {
            IRepositoryUsuario reposi = new RepositoryUsuario();
            usuario.clave = Cryptography.EncrypthAES(usuario.clave);
            reposi.SignIn(usuario);
        }
    }
}
