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
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class INVENTARIO
    {
        public int id_inventario { get; set; }
        public string id_producto { get; set; }
        public int id_sucursal { get; set; }
        public int stoc_minimo { get; set; }
        public int stock_maximo { get; set; }
        public int cantidad { get; set; }
        [JsonIgnore]
        public virtual PRODUCTO PRODUCTO { get; set; }
        [JsonIgnore]
        public virtual SUCURSAL SUCURSAL { get; set; }
    }
}
