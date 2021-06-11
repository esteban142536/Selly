﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infraestructure.Repository
{
    public class RepositoryProducto : IRepositoryProducto
    {
        public void guardarProducto(producto producto)
        {

            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {
                    cdt.producto.Add(producto);
                    cdt.SaveChanges();
                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }

        }

        public IEnumerable<producto> listadoProducto()
        {
            IEnumerable<producto> lista=null;
            IEnumerable<proveedor> listaproveedor = null;

            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;

                try
                {

                    lista = cdt.producto.Include(x=>x.TipoCategoria).ToList();
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

        public producto obtenerProductoID(int id)
        {
            producto producto = null;
            using (contextData cdt = new contextData())
            {
                cdt.Configuration.LazyLoadingEnabled = false;
                producto = cdt.producto.Include(x => x.TipoCategoria).Include(x=>x.proveedor).Include(x => x.productoEstante).Include("productoEstante.estante")
                    .Where(x => x.id == id).FirstOrDefault();
            }
            return producto;
            }
    }
}
