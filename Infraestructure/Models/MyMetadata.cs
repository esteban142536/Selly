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
        public string fecha { get; set; }

        [Display(Name = "Total")]
        public Nullable<double> totalPagado { get; set; }

        [Display(Name = "Comentario")]
        public string comentario { get; set; }

        [Display(Name = "IVA")]
        public Nullable<double> iva { get; set; }

        [Display(Name = "Tienda")]
        public virtual tienda tienda { get; set; }

        [Display(Name = "Usuario")]
        public virtual usuario usuario { get; set; }

        [Display(Name = "Tipo de movimiento")]
        public virtual TipoMovimiento TipoMovimiento { get; set; }
    }

    internal partial class TipoInventarioMetadata
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
        public string nombreEmpresa { get; set; }

        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "País")]
        public virtual pais pais { get; set; }
    }

    internal partial class PaisMetadata
    {
        public int id { get; set; }
        public virtual ICollection<proveedor> proveedor { get; set; }

        [Display(Name = "País")]
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
        public string nombre { get; set; }

        [Display(Name = "Total de stock")]
        public int totalStock { get; set; }

        [Display(Name = "Cantidad máxima")]
        public int cantMaxima { get; set; }

        [Display(Name = "Cantidad mínima")]
        public int cantMinima { get; set; }

        [Display(Name = "Costo unitario")]
        public double costoUnitario { get; set; }

        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        [Display(Name = "Imagen")]
        public byte[] imagen { get; set; }
    }

    internal partial class TipoCategoriaMetadata
    {
        public int id { get; set; }
        public virtual ICollection<producto> producto { get; set; }

        [Display(Name = "Categoría")]
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
        public string email { get; set; }

        [Display(Name = "Contraseña")]
        public string clave { get; set; }

        [Display(Name = "Estado de la cuenta")]
        public bool esActivo { get; set; }
    }

    internal partial class ContactoMetadata
    {
        public int idProveedor { get; set; }
        public virtual proveedor proveedor { get; set; }

        [Display(Name = "Nombre del contacto")]
        public string nombre { get; set; }

        [Display(Name = "Número de teléfono")]
        public int numero { get; set; }

        [Display(Name = "Correo electrónico")]
        public string correo { get; set; }
    }
}
