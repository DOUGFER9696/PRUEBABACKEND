using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RenoExpress.Models;

namespace RenoExpress.Areas.HelpPage.Controllers
{
    public class INVENTARIOController : ApiController
    {
        private RenoExpressEntities db = new RenoExpressEntities();

        // GET: api/INVENTARIO
        public IQueryable<INVENTARIO> GetINVENTARIOs()
        {
            return db.INVENTARIOs;
        }

        // GET: api/INVENTARIO/5
        [ResponseType(typeof(INVENTARIO))]
        public async Task<IHttpActionResult> GetINVENTARIO(int id)
        {
            INVENTARIO iNVENTARIO = await db.INVENTARIOs.FindAsync(id);
            if (iNVENTARIO == null)
            {
                return NotFound();
            }

            return Ok(iNVENTARIO);
        }

        // PUT: api/INVENTARIO/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutINVENTARIO(int id, INVENTARIO iNVENTARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iNVENTARIO.id_inventario)
            {
                return BadRequest();
            }

            db.Entry(iNVENTARIO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!INVENTARIOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/INVENTARIO
        [ResponseType(typeof(INVENTARIO))]
        public async Task<IHttpActionResult> PostINVENTARIO(INVENTARIO iNVENTARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.INVENTARIOs.Add(iNVENTARIO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = iNVENTARIO.id_inventario }, iNVENTARIO);
        }

        // DELETE: api/INVENTARIO/5
        [ResponseType(typeof(INVENTARIO))]
        public async Task<IHttpActionResult> DeleteINVENTARIO(int id)
        {
            INVENTARIO iNVENTARIO = await db.INVENTARIOs.FindAsync(id);
            if (iNVENTARIO == null)
            {
                return NotFound();
            }

            db.INVENTARIOs.Remove(iNVENTARIO);
            await db.SaveChangesAsync();

            return Ok(iNVENTARIO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool INVENTARIOExists(int id)
        {
            return db.INVENTARIOs.Count(e => e.id_inventario == id) > 0;
        }
    }
}