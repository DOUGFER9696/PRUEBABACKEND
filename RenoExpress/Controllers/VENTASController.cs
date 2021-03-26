using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RenoExpress.Models;
using System.Data.SqlClient;

namespace RenoExpress.Controllers
{
    public class VENTASController : Controller
    {
        private RenoExpressEntities db = new RenoExpressEntities();
        private CONEXION_SQLSERVER conexion = new CONEXION_SQLSERVER();
        private Ventas venta;
        private SqlCommand comando;
        private DataTable informacion;
        // GET: VENTAS
        public async Task<ActionResult> Index()
        {
            var eNCABEZADO_FACTURA = db.ENCABEZADO_FACTURA.Include(e => e.CLIENTE).Include(e => e.SUCURSAL);
            return View(await eNCABEZADO_FACTURA.ToListAsync());
        }

        // GET: VENTAS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            comando = new SqlCommand();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_VENTASID";
            comando.Parameters.AddWithValue("@id_encabezado_factura", id);
            informacion = conexion.EjecutarComandosInformacion(comando);
            List<Ventas> ventas = new List<Ventas>();
            foreach (DataRow row in informacion.Rows)
            {
                venta = new Ventas();
                venta.Id_encabezado_factura = Convert.ToInt32(row["id_encabezado_factura"]);
                venta.Fecha_venta = Convert.ToDateTime(row["fecha_venta"]);
                venta.Id_cliente_nit = row["id_cliente_nit"].ToString();
                venta.Id_sucursal = Convert.ToInt32(row["id_sucursal"]);
                venta.Id_detalle_factura = Convert.ToInt32(row["id_detalle_factura"]);
                venta.Id_producto = row["id_producto"].ToString();
                venta.Cantidad = Convert.ToInt32(row["cantidad"]);
                venta._Total = Convert.ToInt32(row["_total"]);
                venta.Iva = float.Parse(row["iva"].ToString());
                venta.Total = float.Parse(row["iva"].ToString());
                ventas.Add(venta);
            }
            // ENCABEZADO_FACTURA eNCABEZADO_FACTURA = await db.ENCABEZADO_FACTURA.FindAsync(id);
            if (ventas == null)
            {
                return HttpNotFound();
            }
            return View(ventas.ToList());
        }

        // GET: VENTAS/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente_nit = new SelectList(db.CLIENTEs, "id_cliente_nit", "nombre");
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre");
            return View();
        }

        // POST: VENTAS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_cliente_nit,id_sucursal")] ENCABEZADO_FACTURA eNCABEZADO_FACTURA)
        {
            if (ModelState.IsValid)
            {
                comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = " SELECT TOP 1 * FROM ENCABEZADO_FACTURA ORDER BY id_encabezado_factura DESC";
                informacion = conexion.EjecutarComandosInformacion(comando);
                if (informacion.Rows.Count > 1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    foreach (DataRow row in informacion.Rows)
                    {
                        eNCABEZADO_FACTURA.id_encabezado_factura = Convert.ToInt32(row["id_encabezado_factura"]) + 1;


                    }
                }
                eNCABEZADO_FACTURA.fecha_venta = DateTime.Now.Date;
                eNCABEZADO_FACTURA.iva = 0;
                eNCABEZADO_FACTURA.total_venta = 0;
                db.ENCABEZADO_FACTURA.Add(eNCABEZADO_FACTURA);
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            ViewBag.id_cliente_nit = new SelectList(db.CLIENTEs, "id_cliente_nit", "nombre", eNCABEZADO_FACTURA.id_cliente_nit);
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre", eNCABEZADO_FACTURA.id_sucursal);
            return View(eNCABEZADO_FACTURA);
        }

        // GET: VENTAS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENCABEZADO_FACTURA eNCABEZADO_FACTURA = await db.ENCABEZADO_FACTURA.FindAsync(id);
            if (eNCABEZADO_FACTURA == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente_nit = new SelectList(db.CLIENTEs, "id_cliente_nit", "nombre", eNCABEZADO_FACTURA.id_cliente_nit);
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre", eNCABEZADO_FACTURA.id_sucursal);
            return View(eNCABEZADO_FACTURA);
        }

        // POST: VENTAS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_encabezado_factura,fecha_venta,id_cliente_nit,id_sucursal,iva,total_venta")] ENCABEZADO_FACTURA eNCABEZADO_FACTURA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eNCABEZADO_FACTURA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente_nit = new SelectList(db.CLIENTEs, "id_cliente_nit", "nombre", eNCABEZADO_FACTURA.id_cliente_nit);
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre", eNCABEZADO_FACTURA.id_sucursal);
            return View(eNCABEZADO_FACTURA);
        }

        // GET: VENTAS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENCABEZADO_FACTURA eNCABEZADO_FACTURA = await db.ENCABEZADO_FACTURA.FindAsync(id);
            
            if (eNCABEZADO_FACTURA == null)
            {
                return HttpNotFound();
            }
            return View(eNCABEZADO_FACTURA);
        }

        // POST: VENTAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ENCABEZADO_FACTURA eNCABEZADO_FACTURA = await db.ENCABEZADO_FACTURA.FindAsync(id);
            db.ENCABEZADO_FACTURA.Remove(eNCABEZADO_FACTURA);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: VENTAS/DETALLESVENTA/5
        public async Task<ActionResult> DETALLESVENTA()
        {
            
         
            ViewBag.id_producto = new SelectList(db.PRODUCTOes, "id_producto", "nombre");

            return View();
        }


        [HttpPost, ActionName("DETALLESVENTA")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GUARDARVENTA([Bind(Include = "id_producto,cantidad")] DETALLE_FACTURA detallefactura)
        {
            if (ModelState.IsValid)
            {
                comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = " SELECT TOP 1 * FROM ENCABEZADO_FACTURA ORDER BY id_encabezado_factura DESC";
                informacion = conexion.EjecutarComandosInformacion(comando);

              

                PRODUCTO producto = await db.PRODUCTOes.FindAsync(detallefactura.id_producto);
                
                detallefactura.total = detallefactura.cantidad * producto.precio_venta;
                if (informacion.Rows.Count > 1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    foreach (DataRow row in informacion.Rows)
                    {
                        detallefactura.id_encabezado_factura = Convert.ToInt32(row["id_encabezado_factura"]);


                    }
                }
                comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = " SELECT TOP 1 * FROM DETALLE_FACTURA ORDER BY id_detalle_factura DESC";
                informacion = conexion.EjecutarComandosInformacion(comando);
                if (informacion.Rows.Count > 1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    foreach (DataRow row in informacion.Rows)
                    {
                        detallefactura.id_detalle_factura = Convert.ToInt32(row["id_detalle_factura"])+1;


                    }
                }
                db.DETALLE_FACTURA.Add(detallefactura);
                await db.SaveChangesAsync();
                return RedirectToAction("DETALLESVENTA");
            }

            ViewBag.id_producto = new SelectList(db.PRODUCTOes, "id_producto", "nombre");
            return View(detallefactura);
        }

        // GET: VENTAS/DETALLESVENTA/5
        public async Task<ActionResult> COBRAR(int? id)
        {
            comando = new SqlCommand();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_CobraFactura";
            comando.Parameters.AddWithValue("@id_encabezado_factura", id);
            conexion.EjecutarComandos(comando);
            return RedirectToAction("Index");
        }

        // GET: VENTAS/DETALLESVENTA/5
        public async Task<ActionResult> ELIMINARDETALLE(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETALLE_FACTURA dETALLE_FACTURA = await db.DETALLE_FACTURA.FindAsync(id);
            
            if (dETALLE_FACTURA == null)
            {
                return HttpNotFound();
            }

            db.DETALLE_FACTURA.Remove(dETALLE_FACTURA);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

    }
}
