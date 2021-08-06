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

        public void editarUsuario(usuario usuarioEdicion)
        {
             reposi.editarUsuario(usuarioEdicion);
        }

        public IEnumerable<usuario> listadoUsuario()
        {
            return reposi.listadoUsuario();
        }

        public usuario logIn(string email, string clave)
        {
            String encript = Cryptography.EncrypthAES(clave);
            return reposi.logIn(email, encript);
        }

        public usuario obtenerUsuarioxID(int id)
        {
            return reposi.obtenerUsuarioxID(id);
        }

        public void SignIn(usuario usuario)
        {
            usuario.clave = Cryptography.EncrypthAES(usuario.clave);
            reposi.SignIn(usuario);
        }
    }
}
