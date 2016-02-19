using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using TimeView.context;
using TimeView.data;

namespace TimeView.api.Controllers
{
    public class OpenDataController : ApiController
    {
        private readonly TimeViewContext db = new TimeViewContext();

        /// <summary>
        ///     Returns all companies (after retrieving from opendata)
        /// </summary>
        /// <returns></returns>
        public IQueryable<Company> GetCompanies()
        {
            //
            // Defaults
            //

            var hospitals_url = "http://opendata.brussel.be/api/records/1.0/search/?dataset=openbare-ziekenhuizen";
            var retirementhomes_url =
                "http://opendata.brussel.be/api/records/1.0/search/?dataset=rusthuizen-voor-ouderen";
            var defaultCategoryId = 1;


            //
            // Hospitals
            //
            var json = "";
            using (var wc = new WebClient())
            {
                json = wc.DownloadString(hospitals_url);
            }

            var hospitals = JsonConvert.DeserializeObject<Hospital>(json);

            foreach (var hospital in hospitals.records)
            {
                var company = new Company
                {
                    Name = hospital.fields.naam,
                    CategoryId = defaultCategoryId,
                    Category = db.Category.Find(defaultCategoryId)
                };
                var exists = db.Company.Where(c => c.Name == hospital.fields.naam).FirstOrDefault();
                if (exists == null)
                {
                    db.Company.Add(company);
                }
            }

            //
            // Retirement homes
            //

            json = "";
            using (var wc = new WebClient())
            {
                json = wc.DownloadString(retirementhomes_url);
            }

            var retirementHomes = JsonConvert.DeserializeObject<Hospital>(json);
            foreach (var retirementHome in retirementHomes.records)
            {
                var company = new Company
                {
                    Name = retirementHome.fields.naam,
                    CategoryId = defaultCategoryId,
                    Category = db.Category.Find(defaultCategoryId)
                };
                var exists = db.Company.Where(c => c.Name == retirementHome.fields.naam).FirstOrDefault();
                if (exists == null)
                {
                    db.Company.Add(company);
                }
            }


            db.SaveChanges();
            return db.Company;
        }


        // GET: api/OpenData/5
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

        // PUT: api/OpenData/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutCompany(int id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.Id)
            {
                return BadRequest();
            }

            db.Entry(company).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OpenData
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

        // DELETE: api/OpenData/5
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