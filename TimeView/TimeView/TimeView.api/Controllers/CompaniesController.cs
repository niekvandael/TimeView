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
    public class CompaniesController : ApiController
    {
        private readonly TimeViewContext db = new TimeViewContext();

        // GET: api/Companies
        public IQueryable<Company> GetCompany()
        {
            // Call the open data controller to retrieve (new) companies
            var odc = new OpenDataController();
            odc.GetCompany();

            return db.Company.Include(c => c.Category);
        }

        // GET: api/Companies/5
        [ResponseType(typeof (Company))]
        public IHttpActionResult GetCompany(int id)
        {
            var company = db.Company.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/Companies/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(company).State = EntityState.Modified;

            foreach (var comp in db.Company)
            {
                if (comp.Name.Equals(company.Name))
                {
                    return StatusCode(HttpStatusCode.Conflict);
                }
            }

            db.Company.Add(company);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Companies
        [ResponseType(typeof (Company))]
        public IHttpActionResult PostCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Company.Add(company);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new {id = company.Id}, company);
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof (Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
            var company = db.Company.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            db.Company.Remove(company);
            db.SaveChanges();

            return Ok(company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyExists(int id)
        {
            return db.Company.Count(e => e.Id == id) > 0;
        }
    }
}