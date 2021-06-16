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
            IRepositoryUsuario reposi = new RepositoryUsuario();
      
        public usuario logIn(string email, string clave)
        {
            String encript = Cryptography.EncrypthAES(clave);
            return reposi.logIn(email, encript);
        }

        public void SignIn(usuario usuario)
        {
            usuario.clave = Cryptography.EncrypthAES(usuario.clave);
            reposi.SignIn(usuario);
        }
    }
}
