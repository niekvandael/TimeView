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
    public class CategoriesController : ApiController
    {
        private readonly TimeViewContext db = new TimeViewContext();

        // GET: api/Categories
        public IQueryable<Category> GetCategory()
        {
            return db.Category;
        }

        // GET: api/Categories/5
        [ResponseType(typeof (Category))]
        public IHttpActionResult GetCategory(int id)
        {
            var category = db.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Categories
        [ResponseType(typeof (Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Category.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new {id = category.Id}, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof (Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            var category = db.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Category.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Category.Count(e => e.Id == id) > 0;
        }
    }
}