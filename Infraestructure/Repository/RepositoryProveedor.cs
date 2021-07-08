using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public class RepositoryProveedor : IRepositoryProveedor
    {
        public IEnumerable<proveedor> listadoProveedor()
        {
            IEnumerable<proveedor> lista = null;

            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    lista = cdt.proveedor.Include(x => x.contactos).Include(x => x.pais).ToList();
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

        public proveedor obtenerProveedorID(int id)
        {
            proveedor proveedor = null;
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;
                proveedor = cdt.proveedor.Include(x => x.contactos).Include(x => x.producto).Where(x => x.id == id).FirstOrDefault();
            }
            return proveedor;
        }

        public void guardarProveedor(proveedor proveedor)
        {
            proveedor proveedorExiste = obtenerProveedorID(proveedor.id);

            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    if (proveedorExiste == null)
                    {
                        //Guarda el proveedor
                        cdt.proveedor.Add(proveedor);
                        cdt.SaveChanges();
                    }
                    else
                    {
                        //Guarda el proveedor modificado
                        cdt.proveedor.Add(proveedor);
                        cdt.Entry(proveedor).State = EntityState.Modified;
                        cdt.SaveChanges();
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

        public proveedor ModificarProveedor(int id)
        {
            throw new NotImplementedException();
        }
    }
}
