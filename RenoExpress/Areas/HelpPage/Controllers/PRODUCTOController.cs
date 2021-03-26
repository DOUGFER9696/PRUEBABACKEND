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
    public class PRODUCTOController : ApiController
    {
        private RenoExpressEntities db = new RenoExpressEntities();

        // GET: api/PRODUCTO
        public IQueryable<PRODUCTO> GetPRODUCTOes()
        {
            return db.PRODUCTOes;
        }

        // GET: api/PRODUCTO/5
        [ResponseType(typeof(PRODUCTO))]
        public async Task<IHttpActionResult> GetPRODUCTO(string id)
        {
            PRODUCTO pRODUCTO = await db.PRODUCTOes.FindAsync(id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }

            return Ok(pRODUCTO);
        }

        // PUT: api/PRODUCTO/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPRODUCTO(string id, PRODUCTO pRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pRODUCTO.id_producto)
            {
                return BadRequest();
            }

            db.Entry(pRODUCTO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PRODUCTOExists(id))
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

        // POST: api/PRODUCTO
        [ResponseType(typeof(PRODUCTO))]
        public async Task<IHttpActionResult> PostPRODUCTO(PRODUCTO pRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PRODUCTOes.Add(pRODUCTO);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PRODUCTOExists(pRODUCTO.id_producto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pRODUCTO.id_producto }, pRODUCTO);
        }

        // DELETE: api/PRODUCTO/5
        [ResponseType(typeof(PRODUCTO))]
        public async Task<IHttpActionResult> DeletePRODUCTO(string id)
        {
            PRODUCTO pRODUCTO = await db.PRODUCTOes.FindAsync(id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }

            db.PRODUCTOes.Remove(pRODUCTO);
            await db.SaveChangesAsync();

            return Ok(pRODUCTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PRODUCTOExists(string id)
        {
            return db.PRODUCTOes.Count(e => e.id_producto == id) > 0;
        }
    }
}