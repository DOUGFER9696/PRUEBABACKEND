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
    public class SUCURSALsController : ApiController
    {
        private RenoExpressEntities db = new RenoExpressEntities();

        // GET: api/SUCURSALs
        public IQueryable<SUCURSAL> GetSUCURSALs()
        {
            return db.SUCURSALs;
        }

        // GET: api/SUCURSALs/5
        [ResponseType(typeof(SUCURSAL))]
        public async Task<IHttpActionResult> GetSUCURSAL(int id)
        {
            SUCURSAL sUCURSAL = await db.SUCURSALs.FindAsync(id);
            if (sUCURSAL == null)
            {
                return NotFound();
            }

            return Ok(sUCURSAL);
        }

        // PUT: api/SUCURSALs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSUCURSAL(int id, SUCURSAL sUCURSAL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sUCURSAL.id_sucursal)
            {
                return BadRequest();
            }

            db.Entry(sUCURSAL).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SUCURSALExists(id))
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

        // POST: api/SUCURSALs
        [ResponseType(typeof(SUCURSAL))]
        public async Task<IHttpActionResult> PostSUCURSAL(SUCURSAL sUCURSAL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SUCURSALs.Add(sUCURSAL);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sUCURSAL.id_sucursal }, sUCURSAL);
        }

        // DELETE: api/SUCURSALs/5
        [ResponseType(typeof(SUCURSAL))]
        public async Task<IHttpActionResult> DeleteSUCURSAL(int id)
        {
            SUCURSAL sUCURSAL = await db.SUCURSALs.FindAsync(id);
            if (sUCURSAL == null)
            {
                return NotFound();
            }

            db.SUCURSALs.Remove(sUCURSAL);
            await db.SaveChangesAsync();

            return Ok(sUCURSAL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SUCURSALExists(int id)
        {
            return db.SUCURSALs.Count(e => e.id_sucursal == id) > 0;
        }
    }
}