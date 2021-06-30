using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infraestructure.Repository
{
    public class RepositoryContactos : IRepositoryContactos
    {
        IRepositoryProveedor repositoryProveedor = new RepositoryProveedor();
        public void GuardarContactos(contactos contactos)
        {
            proveedor proveedor = repositoryProveedor.obtenerProveedorID(contactos.idProveedor);
            contactos contractoExiste = obtenerContactoID(contactos.id);

            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    if (contractoExiste == null)
                    {
                        //Guarda el contacto
                        cdt.contactos.Add(contactos);
                        cdt.SaveChanges();
                    }
                    else
                    {
                        //modifica el contacto modificado
                        if (contractoExiste.nombre != contactos.nombre || contractoExiste.idProveedor != contactos.idProveedor || contractoExiste.numero != contactos.numero) { 
                        cdt.contactos.Add(contactos);
                        cdt.Entry(contactos).State = EntityState.Modified;
                        cdt.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;

                }
            }
        }

        public IEnumerable<contactos> listadoContactos()
        {
            IEnumerable<contactos> lista = null;

            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    lista = cdt.contactos.Include(x => x.idProveedor).ToList();
                    return lista;

                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }

        public contactos obtenerContactoID(int id)
        {
            contactos contactos = null;

            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;
                contactos = cdt.contactos.Include(x => x.proveedor).Where(x => x.id == id).FirstOrDefault();
            }
            return contactos;
        }
    }
}
