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
    public class CLIENTEController : ApiController
    {
        private RenoExpressEntities db = new RenoExpressEntities();

        // GET: api/CLIENTE
        public IQueryable<CLIENTE> GetCLIENTEs()
        {
            return db.CLIENTEs;
        }

        // GET: api/CLIENTE/5
        [ResponseType(typeof(CLIENTE))]
        public async Task<IHttpActionResult> GetCLIENTE(string id)
        {
            CLIENTE cLIENTE = await db.CLIENTEs.FindAsync(id);
            if (cLIENTE == null)
            {
                return NotFound();
            }

            return Ok(cLIENTE);
        }

        // PUT: api/CLIENTE/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCLIENTE(string id, CLIENTE cLIENTE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cLIENTE.id_cliente_nit)
            {
                return BadRequest();
            }

            db.Entry(cLIENTE).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CLIENTEExists(id))
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

        // POST: api/CLIENTE
        [ResponseType(typeof(CLIENTE))]
        public async Task<IHttpActionResult> PostCLIENTE(CLIENTE cLIENTE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CLIENTEs.Add(cLIENTE);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CLIENTEExists(cLIENTE.id_cliente_nit))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cLIENTE.id_cliente_nit }, cLIENTE);
        }

        // DELETE: api/CLIENTE/5
        [ResponseType(typeof(CLIENTE))]
        public async Task<IHttpActionResult> DeleteCLIENTE(string id)
        {
            CLIENTE cLIENTE = await db.CLIENTEs.FindAsync(id);
            if (cLIENTE == null)
            {
                return NotFound();
            }

            db.CLIENTEs.Remove(cLIENTE);
            await db.SaveChangesAsync();

            return Ok(cLIENTE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CLIENTEExists(string id)
        {
            return db.CLIENTEs.Count(e => e.id_cliente_nit == id) > 0;
        }
    }
}