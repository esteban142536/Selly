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
    }
