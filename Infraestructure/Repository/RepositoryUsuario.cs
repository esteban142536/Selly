using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public class RepositoryUsuario : IRepositoryUsuario
    {
        public usuario logIn(string email, string clave)
        {
            usuario user = null;    
            using (contextData cdt = new contextData()) {
                cdt.Configuration.LazyLoadingEnabled = false;
                user = cdt.usuario.Where(u=>u.email==email).FirstOrDefault<usuario>();
            }


            return user;
        }
    }
}
