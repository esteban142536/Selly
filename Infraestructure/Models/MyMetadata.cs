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
        public int idTipoMovimiento { get; set; }
        public virtual ICollection<detalleFactura> detalleFactura { get; set; }

        [Display(Name = "Fecha de emisión")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string fecha { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public Nullable<double> totalPagado { get; set; }

        [Display(Name = "Comentario")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string comentario { get; set; }

        [Display(Name = "IVA")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public Nullable<double> iva { get; set; }

        [Display(Name = "Tienda")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public virtual tienda tienda { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public virtual usuario usuario { get; set; }

        [Display(Name = "Tipo de movimiento")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public virtual TipoMovimiento TipoMovimiento { get; set; }
    }

    internal partial class TipoMovimientoMetadata
    {
        public int id { get; set; }
        public virtual ICollection<inventario> inventario { get; set; }

        [Display(Name = "Tipo de salida")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string tipoSalida { get; set; }

        [Display(Name = "Tipo de entrada")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string tipoEntrada { get; set; }
    }

    internal partial class ProveedorMetadata
    {
        public int id { get; set; }
        public int idPais { get; set; }
        public virtual ICollection<contactos> contactos { get; set; }
        public virtual ICollection<detalleFactura> detalleFactura { get; set; }
        public virtual ICollection<producto> producto { get; set; }

        [Display(Name = "Nombre del proveedor")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string nombreEmpresa { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string direccion { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public virtual pais pais { get; set; }
    }

    internal partial class PaisMetadata
    {
        public int id { get; set; }
        public virtual ICollection<proveedor> proveedor { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string nombre { get; set; }
    }

    internal partial class ProductoMetadata
    {
        public int idCategoria { get; set; }
        public int id { get; set; }
        public virtual TipoCategoria TipoCategoria { get; set; }
        public virtual ICollection<productoEstante> productoEstante { get; set; }
        public virtual ICollection<detalleFactura> detalleFactura { get; set; }
        public virtual ICollection<proveedor> proveedor { get; set; }

        [Display(Name = "Nombre del producto")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string nombre { get; set; }

        [Display(Name = "Total de stock")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public int totalStock { get; set; }

        [Display(Name = "Cantidad máxima")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public int cantMaxima { get; set; }

        [Display(Name = "Cantidad mínima")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public int cantMinima { get; set; }

        [Display(Name = "Costo unitario")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public double costoUnitario { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string descripcion { get; set; }

        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public byte[] imagen { get; set; }
    }

    internal partial class TipoCategoriaMetadata
    {
        public int id { get; set; }
        public virtual ICollection<producto> producto { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string Descripcion { get; set; }
    }

    internal partial class UsuarioMetadata
    {
        public int id { get; set; }
        public virtual ICollection<inventario> inventario { get; set; }
        public virtual tipoUsuario tipoUsuario { get; set; }
        public int idTipoUsuario { get; set; }

        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Display(Name = "Apellidos")]
        public string apellidos { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage ="{0} es un campo requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} no tiene formato válido")]
        public string email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string clave { get; set; }

        [Display(Name = "Estado de la cuenta")]
        public bool esActivo { get; set; }
    }

    internal partial class ContactoMetadata
    {
        public int idProveedor { get; set; }
        public virtual proveedor proveedor { get; set; }

        [Display(Name = "Nombre del contacto")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string nombre { get; set; }

        [Display(Name = "Número de teléfono")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public int numero { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string correo { get; set; }
    }
}
