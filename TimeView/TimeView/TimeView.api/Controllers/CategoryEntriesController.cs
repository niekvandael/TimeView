using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using TimeView.context;
using TimeView.data;

namespace TimeView.api.Controllers
{
    public class CategoryEntriesController : ApiController
    {
        private readonly TimeViewContext db = new TimeViewContext();

        // GET: api/CategoryEntries?CategoryId=1
        public IQueryable<CategoryEntry> GetCategoryEntriesForCategory(int categoryId)
        {
            return db.CategoryEntry.Where(c => c.CategoryId == categoryId);
        }

        // GET: api/CategoryEntries
        public IQueryable<CategoryEntry> GetCategoryEntry()
        {
            return db.CategoryEntry;
        }

        // GET: api/CategoryEntries/5
        [ResponseType(typeof (CategoryEntry))]
        public IHttpActionResult GetCategoryEntry(int id)
        {
            var categoryEntry = db.CategoryEntry.Find(id);
            if (categoryEntry == null)
            {
                return NotFound();
            }

            return Ok(categoryEntry);
        }

        // PUT: api/CategoryEntries/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutCategoryEntry(int id, CategoryEntry categoryEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoryEntry.Id)
            {
                return BadRequest();
            }

            db.Entry(categoryEntry).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryEntryExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CategoryEntries
        [ResponseType(typeof (CategoryEntry))]
        public IHttpActionResult PostCategoryEntry(CategoryEntry categoryEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CategoryEntry.Add(categoryEntry);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new {id = categoryEntry.Id}, categoryEntry);
        }

        // DELETE: api/CategoryEntries/5
        [ResponseType(typeof (CategoryEntry))]
        public IHttpActionResult DeleteCategoryEntry(int id)
        {
            var categoryEntry = db.CategoryEntry.Find(id);
            if (categoryEntry == null)
            {
                return NotFound();
            }

            db.CategoryEntry.Remove(categoryEntry);
            db.SaveChanges();

            return Ok(categoryEntry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryEntryExists(int id)
        {
            return db.CategoryEntry.Count(e => e.Id == id) > 0;
        }
    }
}