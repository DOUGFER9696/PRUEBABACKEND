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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace RenoExpress.Controllers
{
    public class INVENTARIOSController : Controller
    {
        private RenoExpressEntities db = new RenoExpressEntities();

        // GET: ConsumirInventario
        public async Task<ActionResult> CONSUMOAPI()
        {
            string baseurl = "https://localhost:44380/";
            List<INVENTARIO> inventario = new List<INVENTARIO>();



            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseurl);
            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage res = await cliente.GetAsync("api/INVENTARIO/");


            if (res.IsSuccessStatusCode)
            {
                var Empresponse = res.Content.ReadAsStringAsync().Result;

                inventario = JsonConvert.DeserializeObject<List<INVENTARIO>>(Empresponse);


            }


            return View(inventario);
        }



        // GET: INVENTARIOS
        public async Task<ActionResult> Index()
        {
            var iNVENTARIOs = db.INVENTARIOs.Include(i => i.PRODUCTO).Include(i => i.SUCURSAL);
            return View(await iNVENTARIOs.ToListAsync());
        }

        // GET: INVENTARIOS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVENTARIO iNVENTARIO = await db.INVENTARIOs.FindAsync(id);
            if (iNVENTARIO == null)
            {
                return HttpNotFound();
            }
            return View(iNVENTARIO);
        }

        // GET: INVENTARIOS/Create
        public ActionResult Create()
        {
            ViewBag.id_producto = new SelectList(db.PRODUCTOes, "id_producto", "nombre");
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre");
            return View();
        }

        // POST: INVENTARIOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_inventario,id_producto,id_sucursal,stoc_minimo,stock_maximo,cantidad")] INVENTARIO iNVENTARIO)
        {
            if (ModelState.IsValid)
            {
                db.INVENTARIOs.Add(iNVENTARIO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_producto = new SelectList(db.PRODUCTOes, "id_producto", "nombre", iNVENTARIO.id_producto);
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre", iNVENTARIO.id_sucursal);
            return View(iNVENTARIO);
        }

        // GET: INVENTARIOS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVENTARIO iNVENTARIO = await db.INVENTARIOs.FindAsync(id);
            if (iNVENTARIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_producto = new SelectList(db.PRODUCTOes, "id_producto", "nombre", iNVENTARIO.id_producto);
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre", iNVENTARIO.id_sucursal);
            return View(iNVENTARIO);
        }

        // POST: INVENTARIOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_inventario,id_producto,id_sucursal,stoc_minimo,stock_maximo,cantidad")] INVENTARIO iNVENTARIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNVENTARIO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.PRODUCTOes, "id_producto", "nombre", iNVENTARIO.id_producto);
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre", iNVENTARIO.id_sucursal);
            return View(iNVENTARIO);
        }

        // GET: INVENTARIOS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVENTARIO iNVENTARIO = await db.INVENTARIOs.FindAsync(id);
            if (iNVENTARIO == null)
            {
                return HttpNotFound();
            }
            return View(iNVENTARIO);
        }

        // POST: INVENTARIOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            INVENTARIO iNVENTARIO = await db.INVENTARIOs.FindAsync(id);
            db.INVENTARIOs.Remove(iNVENTARIO);
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
    }
}
