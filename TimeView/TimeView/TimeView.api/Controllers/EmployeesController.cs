using System;
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
    public class EmployeesController : ApiController
    {
        private readonly TimeViewContext db = new TimeViewContext();

        [ResponseType(typeof (Employee))]
        public Employee GetEmployee(string username, string password)
        {
            try
            {
                var employee =
                    db.Employee.Include(e => e.Company)
                        .Where(e => e.Username == username)
                        .Where(e => e.Password == password)
                        .First();

                employee.Company.Employees = null;

                return employee;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: api/Employees
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employee.Include(e => e.Following);
        }

        // GET: api/Employees/5
        [ResponseType(typeof (Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            var employee = db.Employee
                .Include(e => e.Following.Select(f => f.Company))
                .Include(e => e.Following)
                .Where(e => e.Id == id).First();

// Avoid loops

            for (var i = 0; i < employee.Following.Count; i++)
            {
                employee.Following[i].Follower = null;
                employee.Following[i].Company.Employees = null;
            }

// End avoid loops

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Companies/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var emp in db.Employee)
            {
                if (employee.Username.Equals(emp.Username))
                {
                    return StatusCode(HttpStatusCode.Conflict);
                }
            }

            db.Employee.Add(employee);

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

        // POST: api/Employees
        [ResponseType(typeof (Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employee.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new {id = employee.Id}, employee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof (Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var employee = db.Employee.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employee.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employee.Count(e => e.Id == id) > 0;
        }
    }
}