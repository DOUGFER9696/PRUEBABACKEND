using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenoExpress.Models
{
    public class Ventas
    {
        private int id_encabezado_factura;
        private DateTime fecha_venta;
        private string id_cliente_nit;
        private int id_sucursal;
        private float iva;
        private float total;
        private int id_detalle_factura;
        private string id_producto;
        private int cantidad;
        private float _total;

        public int Id_encabezado_factura { get => id_encabezado_factura; set => id_encabezado_factura = value; }
        public DateTime Fecha_venta { get => fecha_venta; set => fecha_venta = value; }
        public string Id_cliente_nit { get => id_cliente_nit; set => id_cliente_nit = value; }
        public int Id_sucursal { get => id_sucursal; set => id_sucursal = value; }
        public float Iva { get => iva; set => iva = value; }
        public float Total { get => total; set => total = value; }
        public int Id_detalle_factura { get => id_detalle_factura; set => id_detalle_factura = value; }
        public string Id_producto { get => id_producto; set => id_producto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public float _Total { get => _total; set => _total = value; }

        
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual PRODUCTO  PRODUCTO { get; set; }
        public virtual SUCURSAL SUCURSAL { get; set; }

    }
}