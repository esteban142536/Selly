using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class InventarioMetadata {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public int idTienda { get; set; }

        [Display(Name = "Fecha de emicion")]
        public string fecha { get; set; }

        [Display(Name = "Total pagado")]
        public Nullable<double> totalPagado { get; set; }
        [Display(Name = "Comentario del empleado")]
        public string comentario { get; set; }
        [Display(Name = "Impuesto al valor agregado")]
        public Nullable<double> iva { get; set; }
        public int idTipoMovimiento { get; set; }

        public virtual tienda tienda { get; set; }
        public virtual usuario usuario { get; set; }
        public virtual TipoMovimiento TipoMovimiento { get; set; }
        public virtual ICollection<detalleFactura> detalleFactura { get; set; }
    }
    internal partial class TipoInventarioMetadata
    {
        public int id { get; set; }
        [Display(Name = "Tipo de salida")]
        public string tipoSalida { get; set; }
        [Display(Name = "Tipo de entrada")]
        public string tipoEntrada { get; set; }

        public virtual ICollection<inventario> inventario { get; set; }
    }
    internal partial class ProveedorMetadata
    {
        [Display(Name = "Nombre del proveedor")]
        public string nombreEmpresa { get; set; }
        [Display(Name = "Direccion registrada")]
        public string direccion { get; set; }
        public int id { get; set; }
        public int idPais { get; set; }

        public virtual pais pais { get; set; }
        public virtual ICollection<contactos> contactos { get; set; }
        public virtual ICollection<detalleFactura> detalleFactura { get; set; }
        public virtual ICollection<producto> producto { get; set; }
    }
    internal partial class PaisMetadata
    {
        public int id { get; set; }
        [Display(Name = "Pais de procedencia")]
        public string nombre { get; set; }

        public virtual ICollection<proveedor> proveedor { get; set; }
    }
    internal partial class ProductoMetadata
    {
        [Display(Name = "Nombre del producto")]
        public string nombre { get; set; }
        [Display(Name = "Total en almacen")]
        public int totalStock { get; set; }
        [Display(Name = "Capacidad maxima")]
        public int cantMaxima { get; set; }
        [Display(Name = "Capacidad minima")]
        public int cantMinima { get; set; }
        public int idCategoria { get; set; }
        public int id { get; set; }
        [Display(Name = "Costo unitario")]
        public double costoUnitario { get; set; }
        [Display(Name = "Descripcion del producto")]
        public string descripcion { get; set; }
        [Display(Name = "Imagen")]
        public byte[] imagen { get; set; }
        public virtual TipoCategoria TipoCategoria { get; set; }
        public virtual ICollection<productoEstante> productoEstante { get; set; }
        public virtual ICollection<detalleFactura> detalleFactura { get; set; }
        public virtual ICollection<proveedor> proveedor { get; set; }
    }
    internal partial class TipoCategoriaMetadata
    {
        public int id { get; set; }

        [Display(Name = "Categoria")]
        public string Descripcion { get; set; }

        public virtual ICollection<producto> producto { get; set; }
    }
    internal partial class UsuarioMetadata
    {
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Apellidos")]
        public string apellidos { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Clave")]
        public string clave { get; set; }
        public int id { get; set; }
        [Display(Name = "Estado de cuenta")]
        public bool esActivo { get; set; }
        public int idTipoUsuario { get; set; }

        public virtual ICollection<inventario> inventario { get; set; }
        public virtual tipoUsuario tipoUsuario { get; set; }
    }
    internal partial class ContactoMetadata
    {
        [Display(Name = "Nombre del contacto")]
        public string nombre { get; set; }
        [Display(Name = "Numero de telefono")]
        public int numero { get; set; }
        [Display(Name = "Correo electronico")]
        public string correo { get; set; }
        public int idProveedor { get; set; }

        public virtual proveedor proveedor { get; set; }
    }
}
