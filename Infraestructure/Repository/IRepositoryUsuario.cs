using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryUsuario
    {
        usuario logIn(String email, String clave);
        void SignIn(usuario usuario);
        IEnumerable<usuario> listadoUsuario();

        void editarUsuario(usuario usuarioEdicion);
        usuario obtenerUsuarioxID(int id);
    }
}
