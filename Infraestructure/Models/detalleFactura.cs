//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class detalleFactura
    {
        public int idProducto { get; set; }
        public int idInventario { get; set; }
        public double precio { get; set; }
        public int cantidadComprada { get; set; }
        public Nullable<int> idEstante { get; set; }
        public Nullable<int> idProveedor { get; set; }
    
        public virtual estante estante { get; set; }
        public virtual inventario inventario { get; set; }
        public virtual producto producto { get; set; }
        public virtual proveedor proveedor { get; set; }
    }
}
