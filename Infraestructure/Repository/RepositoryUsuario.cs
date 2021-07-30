using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infraestructure.Repository
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public void cambiarEstado(int id)
        {
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    usuario usu = cdt.usuario.Where(x => x.id == id).FirstOrDefault();
                    usu.esActivo = !usu.esActivo;
                    cdt.usuario.Add(usu);

                    cdt.Entry(usu).State = EntityState.Modified;
                    cdt.SaveChanges();

                }
                catch (Exception e)
                {
                    string mensaje = "";
                    Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }

        public IEnumerable<usuario> listadoUsuario()
        {
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    return cdt.usuario.Include(x=>x.tipoUsuario).ToList();
                }
                catch (Exception e)
                {
                    string mensaje = "";
                    Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }

        public usuario logIn(string email, string clave)
        {
            usuario user = null;    
            using (contextData cdt = new contextData()) {
                cdt.Configuration.LazyLoadingEnabled = false;
                user = cdt.usuario.Where(u=>u.email==email && u.clave==clave &&u.esActivo).FirstOrDefault<usuario>();
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
                    string mensaje = "";
                    Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }
    }
}
