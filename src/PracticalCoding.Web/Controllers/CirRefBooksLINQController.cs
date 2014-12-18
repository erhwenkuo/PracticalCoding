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
    public class CirRefBooksLINQController : ApiController
    {
        private EF6DbContext db = new EF6DbContext();

        // GET: api/CirRefBooksLINQ
        public IQueryable<BookDTO> GetCirRefBooks()
        {
            var cirRefBooks = from book in db.CirRefBooks.Include(b => b.Author)
                        select new BookDTO()
                        {
                            Id = book.Id,
                            Title = book.Title,
                            AuthorName = book.Author.Name
                        };

            return cirRefBooks;
        }

        // GET: api/CirRefBooksLINQ/5
        [ResponseType(typeof(BookDetailDTO))]
        public async Task<IHttpActionResult> GetCirRefBook(int id)
        {
            var cirRefBook = await db.CirRefBooks.Include(b => b.Author).Select(book =>
                new BookDetailDTO()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Year = book.Year,
                    Price = book.Price,
                    AuthorName = book.Author.Name,
                    Genre = book.Genre
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (cirRefBook == null)
            {
                return NotFound();
            }

            return Ok(cirRefBook);
        }

        // PUT: api/CirRefBooksLINQ/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCirRefBook(int id, CirRefBook cirRefBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cirRefBook.Id)
            {
                return BadRequest();
            }

            db.Entry(cirRefBook).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CirRefBookExists(id))
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

        // POST: api/CirRefBooksLINQ
        [ResponseType(typeof(CirRefBook))]
        public async Task<IHttpActionResult> PostCirRefBook(CirRefBook cirRefBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CirRefBooks.Add(cirRefBook);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cirRefBook.Id }, cirRefBook);
        }

        // DELETE: api/CirRefBooksLINQ/5
        [ResponseType(typeof(CirRefBook))]
        public async Task<IHttpActionResult> DeleteCirRefBook(int id)
        {
            CirRefBook cirRefBook = await db.CirRefBooks.FindAsync(id);
            if (cirRefBook == null)
            {
                return NotFound();
            }

            db.CirRefBooks.Remove(cirRefBook);
            await db.SaveChangesAsync();

            return Ok(cirRefBook);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CirRefBookExists(int id)
        {
            return db.CirRefBooks.Count(e => e.Id == id) > 0;
        }
    }
}