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

        public int idTienda { get; set; }
        public int idTipoMovimiento { get; set; }
        public virtual ICollection<detalleFactura> detalleFactura { get; set; }

        [Display(Name = "Usuario:")]
        [Required(ErrorMessage = "Obligatorio")]
        public int idUsuario { get; set; }

        [Display(Name = "Fecha de emisión:")]
        [Required(ErrorMessage = "Obligatorio")]
        public string fecha { get; set; }

        [Display(Name = "Tipo de movimiento:")]
        public virtual TipoMovimiento TipoMovimiento { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = "Obligatorio")]
        public Nullable<double> totalPagado { get; set; }

        [Display(Name = "Comentario")]
        [Required(ErrorMessage = "Obligatorio")]
        public string comentario { get; set; }

        [Display(Name = "IVA")]
        [Required(ErrorMessage = "Obligatorio")]
        public Nullable<double> iva { get; set; }

        [Display(Name = "Tienda:")]
        public virtual tienda tienda { get; set; }

        [Display(Name = "Usuario")]

        public virtual usuario usuario { get; set; }
    }

    internal partial class TipoMovimientoMetadata
    {
        public int id { get; set; }
        public virtual ICollection<inventario> inventario { get; set; }

        [Display(Name = "Tipo de salida")]

        public string tipoSalida { get; set; }

        [Display(Name = "Tipo de entrada")]

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
        [Required(ErrorMessage = "Obligatorio")]
        [DataType(DataType.Text, ErrorMessage = "Obligatorio")]
        public string nombreEmpresa { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Obligatorio")]
        [DataType(DataType.Text, ErrorMessage = "Obligatorio")]
        public string direccion { get; set; }

        [Display(Name = "País")]
        public virtual pais pais { get; set; }
    }

    internal partial class PaisMetadata
    {
        public int id { get; set; }
        public virtual ICollection<proveedor> proveedor { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "Obligatorio")]
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
        [Required(ErrorMessage = "Obligatorio")]
        public string nombre { get; set; }

        [Display(Name = "Total de existencia")]
        [Required(ErrorMessage = "Obligatorio")]
        [Range(1, 99, ErrorMessage = "{0} debe ser mayor a 1 y menor a 99")]
        public int totalStock { get; set; }

        [Display(Name = "Cantidad máxima")]
        [Required(ErrorMessage = "Obligatorio")]
        [Range(1, 99, ErrorMessage = "{0} debe ser mayor a 1 y menor a 99")]

        public int cantMaxima { get; set; }

        [Display(Name = "Cantidad mínima")]
        [Required(ErrorMessage = "Obligatorio")]
        [Range(1, 99, ErrorMessage = "{0} debe ser mayor a 1 y menor a 99")]

        public int cantMinima { get; set; }

        [Display(Name = "Costo")]
        [Required(ErrorMessage = "Obligatorio")]
        public double costoUnitario { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Obligatorio")]
        public string descripcion { get; set; }

        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "Obligatorio")]
        public byte[] imagen { get; set; }
    }

    internal partial class TipoCategoriaMetadata
    {
        public int id { get; set; }
        public virtual ICollection<producto> producto { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "Obligatorio")]
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
        [Required(ErrorMessage = "Obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} no tiene formato válido")]
        public string email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Obligatorio")]
        public string clave { get; set; }

        [Display(Name = "Estado de la cuenta")]
        public bool esActivo { get; set; }
    }

    internal partial class ContactoMetadata
    {
        public int id { get; set; }
        public int idProveedor { get; set; }
        public virtual proveedor proveedor { get; set; }

        [Display(Name = "Nombre del contacto")]
        [Required(ErrorMessage = "Obligatorio")]
        public string nombre { get; set; }

        [Display(Name = "Número de teléfono")]
        [Required(ErrorMessage = "Obligatorio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "{0} no tiene formato válido")]
        public int numero { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "Obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} no tiene formato válido")]
        public string correo { get; set; }
    }
}
