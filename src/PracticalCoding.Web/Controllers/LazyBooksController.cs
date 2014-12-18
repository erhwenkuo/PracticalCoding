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
using PracticalCoding.Web.Models;
using PracticalCoding.Web.Models.CodeFirst;

namespace PracticalCoding.Web.Controllers
{
    public class LazyBooksController : ApiController
    {
        private EF6DbContext db = new EF6DbContext();

        // GET: api/LazyBooks
        public IQueryable<LazyBook> GetLazyBooks()
        {
            return db.LazyBooks;
        }

        // GET: api/LazyBooks/5
        [ResponseType(typeof(LazyBook))]
        public async Task<IHttpActionResult> GetLazyBook(int id)
        {
            LazyBook lazyBook = await db.LazyBooks.FindAsync(id);
            if (lazyBook == null)
            {
                return NotFound();
            }

            return Ok(lazyBook);
        }

        // PUT: api/LazyBooks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLazyBook(int id, LazyBook lazyBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lazyBook.Id)
            {
                return BadRequest();
            }

            db.Entry(lazyBook).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LazyBookExists(id))
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

        // POST: api/LazyBooks
        [ResponseType(typeof(LazyBook))]
        public async Task<IHttpActionResult> PostLazyBook(LazyBook lazyBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LazyBooks.Add(lazyBook);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lazyBook.Id }, lazyBook);
        }

        // DELETE: api/LazyBooks/5
        [ResponseType(typeof(LazyBook))]
        public async Task<IHttpActionResult> DeleteLazyBook(int id)
        {
            LazyBook lazyBook = await db.LazyBooks.FindAsync(id);
            if (lazyBook == null)
            {
                return NotFound();
            }

            db.LazyBooks.Remove(lazyBook);
            await db.SaveChangesAsync();

            return Ok(lazyBook);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LazyBookExists(int id)
        {
            return db.LazyBooks.Count(e => e.Id == id) > 0;
        }
    }
}