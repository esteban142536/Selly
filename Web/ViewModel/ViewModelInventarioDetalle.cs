using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelInventarioDetalle
    {
        public string nombre { get; set; }
        public int totalStock { get; set; }
        public int cantMaxima { get; set; }
        public int cantMinima { get; set; }
        public int idCategoria { get; set; }
        public int id { get; set; }
        public double costoUnitario { get; set; }
        public string descripcion { get; set; }
        public byte[] imagen { get; set; }
        public virtual producto Producto { get; set; }
        
        public double Precio
        {
            get { return Producto.costoUnitario; }
        }

        public double SubTotal
        {
            get
            {
                return calculoSubtotal();
            }
        }

        private double calculoSubtotal()
        {
            return ((this.Precio * this.totalStock) * 0.13) + this.Precio;
        }

        public ViewModelInventarioDetalle(int idProducto)
        {
            IServiseProducto serviseProducto = new ServiseProducto();
            this.id = idProducto;
            this.Producto = serviseProducto.obtenerProductoID(idProducto);
        }
    }
}