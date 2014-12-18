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
using AutoMapper;

namespace PracticalCoding.Web.Controllers
{
    public class CirRefBooksController : ApiController
    {
        private EF6DbContext db = new EF6DbContext();

        // GET: api/CirRefBooks
        public IQueryable<BookDTO> GetCirRefBooks()
        {
            Mapper.CreateMap<CirRefBook, BookDTO>();
            return Mapper.Map<IList<BookDTO>>(db.CirRefBooks.Include(b => b.Author)).AsQueryable();
        }

        // GET: api/CirRefBooks/5
        [ResponseType(typeof(BookDetailDTO))]
        public async Task<IHttpActionResult> GetCirRefBook(int id)
        {
            Mapper.CreateMap<CirRefBook, BookDetailDTO>();
            var cirRefBook = await db.CirRefBooks.FindAsync(id);
            if (cirRefBook == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<CirRefBook, BookDetailDTO>(cirRefBook));
        }

        // PUT: api/CirRefBooks/5
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

        // POST: api/CirRefBooks
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

        // DELETE: api/CirRefBooks/5
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