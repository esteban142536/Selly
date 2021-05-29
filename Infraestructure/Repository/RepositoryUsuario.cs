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
                user = cdt.usuario.Where(u=>u.email==email && u.clave==clave).FirstOrDefault<usuario>();
            }


            return user;
        }

        public void SignIn(usuario usuario)
        {
            usuario user = null;
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    user = cdt.usuario.Add(usuario);
                    cdt.SaveChanges(); //solo es nesesario para insert, delete y update
                }
                catch (Exception e) {
                  Console.WriteLine(e);
                }
            }
        }
    }
}
