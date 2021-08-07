using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Util;

namespace Web.ViewModel
{
    public class Carrito
    {
        public List<ViewModelInventarioDetalle> Items { get; private set; }

        //Implementación Singleton

        // Las propiedades de solo lectura solo se pueden establecer en la inicialización o en un constructor
        public static readonly Carrito Instancia;

        // Se llama al constructor estático tan pronto como la clase se carga en la memoria
        static Carrito()
        {
            // Si el carrito no está en la sesión, cree uno y guarde los items.
            if (HttpContext.Current.Session["carrito"] == null)
            {
                Instancia = new Carrito();
                Instancia.Items = new List<ViewModelInventarioDetalle>();
                HttpContext.Current.Session["carrito"] = Instancia;
            }
            else
            {
                // De lo contrario, obténgalo de la sesión.
                Instancia = (Carrito)HttpContext.Current.Session["carrito"];
            }
        }

        // Un constructor protegido asegura que un objeto no se puede crear desde el exterior
        protected Carrito() { }

        /**
         * AgregarItem (): agrega un artículo a la compra
         */
        public String AgregarItem(int productoID)
        {
            String mensaje = "";
            // Crear un nuevo artículo para agregar al carrito
            ViewModelInventarioDetalle nuevoItem = new ViewModelInventarioDetalle(productoID);
            // Si este artículo ya existe en lista de libros, aumente la Cantidad
            // De lo contrario, agregue el nuevo elemento a la lista
            if (nuevoItem != null)
            {
                if (Items.Exists(x => x.id == productoID))
                {
                    ViewModelInventarioDetalle item = Items.Find(x => x.id == productoID);
                    item.totalStock++;
                }
                else
                {
                    nuevoItem.totalStock = 1;
                    Items.Add(nuevoItem);
                }
                mensaje = SweetAlertHelper.Mensaje("Orden producto", "Producto agregado a la orden", SweetAlertMessageType.success);
            }
            else
            {
                mensaje = SweetAlertHelper.Mensaje("Orden producto", "El producto solicitado no existe", SweetAlertMessageType.warning);
            }
            return mensaje;
        }

        /**
         * SetItemCantidad(): cambia la Cantidad de un artículo en el carrito
         */
        public void SetItemCantidad(int idProducto, int Cantidad)
        {
            String mensaje = "";
            // Si estamos configurando la Cantidad a 0, elimine el artículo por completo
            ViewModelInventarioDetalle actualizarItem = new ViewModelInventarioDetalle(idProducto);
            if (Cantidad <= actualizarItem.Producto.totalStock)
            {

                if (Cantidad == 0)
                {
                    EliminarItem(idProducto);
                }
                else
                {
                    // Encuentra el artículo y actualiza la Cantidad
                    if (Items.Exists(x => x.id == idProducto))
                    {
                        ViewModelInventarioDetalle item = Items.Find(x => x.id == idProducto);
                        item.totalStock = Cantidad;


                    }
                }

            }

        }

        public String EliminarItem(int idProducto)
        {
            String mensaje = "El producto no existe";
            if (Items.Exists(x => x.id == idProducto))
            {
                var itemEliminar = Items.Single(x => x.id == idProducto);
                Items.Remove(itemEliminar);
                mensaje = SweetAlertHelper.Mensaje("Orden producto", "producto eliminado", SweetAlertMessageType.success);
            }
            return mensaje;
        }

        public double GetTotal()
        {
            double total = 0;
            total = Items.Sum(x => x.SubTotal);
            return total;
        }

        public double GetIva()
        {
            double total = 0;
            total = Items.Sum(x => x.Precio);

            return total;
        }

        public int GetCountItems()
        {
            int total = 0;
            total = Items.Sum(x => x.totalStock);
            return total;
        }

        public void eliminarCarrito()
        {
            Items.Clear();
        }
    }
}