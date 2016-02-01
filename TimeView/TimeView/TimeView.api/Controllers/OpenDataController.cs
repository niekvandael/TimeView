using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TimeView.context;
using TimeView.data;

namespace TimeView.api.Controllers
{
    public class OpenDataController : ApiController
    {
        private TimeView.context.TimeViewContext db = new TimeViewContext();

        /// <summary>
        /// Returns an individual Product.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Company> GetCompany()
        {
            //
            // Defaults
            //

            string hospitals_url = "http://opendata.brussel.be/api/records/1.0/search/?dataset=openbare-ziekenhuizen";
            string retirementhomes_url = "http://opendata.brussel.be/api/records/1.0/search/?dataset=rusthuizen-voor-ouderen";
            int defaultCategoryId = 1;


            //
            // Hospitals
            //
            string json = "";
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(hospitals_url);
            }

            Hospital hospitals = JsonConvert.DeserializeObject<Hospital>(json);

            foreach (Record hospital in hospitals.records) {
                Company company = new Company { Name = hospital.fields.naam , CategoryId=defaultCategoryId, Category=db.Category.Find(defaultCategoryId) };
                Company exists = db.Company.Where(c => c.Name == hospital.fields.naam).FirstOrDefault();
                if (exists == null) {
                    db.Company.Add(company);
                }
            }

            //
            // Retirement homes
            //

            json = "";
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(retirementhomes_url);
            }

            Hospital retirementHomes = JsonConvert.DeserializeObject<Hospital>(json);
            foreach (Record retirementHome in retirementHomes.records)
            {
                Company company = new Company { Name = retirementHome.fields.naam, CategoryId = defaultCategoryId, Category = db.Category.Find(defaultCategoryId) };
                Company exists = db.Company.Where(c => c.Name == retirementHome.fields.naam).FirstOrDefault();
                if (exists == null)
                {
                    db.Company.Add(company);
                }
            }



            db.SaveChanges();
            return db.Company;
        }

       
        // GET: api/OpenData/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(int id)
        {
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/OpenData/5
        [ResponseType(typeof(void))]
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
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OpenData
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Company.Add(company);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = company.Id }, company);
        }

        // DELETE: api/OpenData/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
            Company company = db.Company.Find(id);
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