//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RenoExpress.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ENCABEZADO_FACTURA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ENCABEZADO_FACTURA()
        {
            this.DETALLE_FACTURA = new HashSet<DETALLE_FACTURA>();
        }
    
        public int id_encabezado_factura { get; set; }
        public System.DateTime fecha_venta { get; set; }
        public string id_cliente_nit { get; set; }
        public int id_sucursal { get; set; }
        public Nullable<double> iva { get; set; }
        public Nullable<double> total_venta { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_FACTURA> DETALLE_FACTURA { get; set; }
        public virtual SUCURSAL SUCURSAL { get; set; }
    }
}
