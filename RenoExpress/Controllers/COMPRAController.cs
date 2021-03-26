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

namespace RenoExpress.Controllers
{
    public class COMPRAController : Controller
    {
        private RenoExpressEntities db = new RenoExpressEntities();

        // GET: COMPRA
        public async Task<ActionResult> Index()
        {
            var cOMPRAs = db.COMPRAs.Include(c => c.PRODUCTO).Include(c => c.SUCURSAL);
            return View(await cOMPRAs.ToListAsync());
        }

        // GET: COMPRA/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPRA cOMPRA = await db.COMPRAs.FindAsync(id);
            if (cOMPRA == null)
            {
                return HttpNotFound();
            }
            return View(cOMPRA);
        }

        // GET: COMPRA/Create
        public ActionResult Create()
        {
            ViewBag.id_producto = new SelectList(db.PRODUCTOes, "id_producto", "nombre");
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre");
            return View();
        }

        // POST: COMPRA/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_compra,id_sucursal,id_producto,cantidad,precio_unitario")] COMPRA cOMPRA)
        {
            if (ModelState.IsValid)
            {
                db.COMPRAs.Add(cOMPRA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_producto = new SelectList(db.PRODUCTOes, "id_producto", "nombre", cOMPRA.id_producto);
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre", cOMPRA.id_sucursal);
            return View(cOMPRA);
        }

        // GET: COMPRA/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPRA cOMPRA = await db.COMPRAs.FindAsync(id);
            if (cOMPRA == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_producto = new SelectList(db.PRODUCTOes, "id_producto", "nombre", cOMPRA.id_producto);
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre", cOMPRA.id_sucursal);
            return View(cOMPRA);
        }

        // POST: COMPRA/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_compra,id_sucursal,id_producto,cantidad,precio_unitario")] COMPRA cOMPRA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMPRA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.PRODUCTOes, "id_producto", "nombre", cOMPRA.id_producto);
            ViewBag.id_sucursal = new SelectList(db.SUCURSALs, "id_sucursal", "nombre", cOMPRA.id_sucursal);
            return View(cOMPRA);
        }

        // GET: COMPRA/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPRA cOMPRA = await db.COMPRAs.FindAsync(id);
            if (cOMPRA == null)
            {
                return HttpNotFound();
            }
            return View(cOMPRA);
        }

        // POST: COMPRA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            COMPRA cOMPRA = await db.COMPRAs.FindAsync(id);
            db.COMPRAs.Remove(cOMPRA);
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
