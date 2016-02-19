using System;
using System.Collections.Generic;
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
    public class SchedulesController : ApiController
    {
        private readonly TimeViewContext db = new TimeViewContext();

        // GET: api/Schedules
        public IQueryable<Schedule> GetScheduleForEmployee(int employeeId)
        {
            var selectFrom = DateTime.Now.AddDays(-7);
            return
                db.Schedule.Where(s => s.EmployeeId == employeeId)
                    .Where(s => s.Day >= selectFrom)
                    .Include(s => s.CategoryEntry)
                    .OrderBy(s => s.Day);
        }

        // GET: api/Schedules
        public IQueryable<Schedule> GetSchedule()
        {
            return db.Schedule;
        }

        // GET: api/Schedules/5
        [ResponseType(typeof (Schedule))]
        public IHttpActionResult GetSchedule(int id)
        {
            var schedule = db.Schedule.Find(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return Ok(schedule);
        }

        // PUT: api/Schedules/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutSchedules(Schedule schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Entry(schedule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(schedule.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Schedules
        [ResponseType(typeof (Schedule))]
        public IHttpActionResult PostSchedule(List<Schedule> schedules)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var schedule in schedules)
            {
                schedule.CategoryEntry = null; // Omit add/update of FK

                if (schedule.Id == -1)
                {
                    db.Schedule.Add(schedule);
                    db.Entry(schedule).State = EntityState.Added;
                }
                else
                {
                    db.Entry(schedule).State = EntityState.Modified;
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(schedules);
        }

        // DELETE: api/Schedules/5
        [ResponseType(typeof (Schedule))]
        public IHttpActionResult DeleteSchedule(int id)
        {
            var schedule = db.Schedule.Find(id);
            if (schedule == null)
            {
                return NotFound();
            }

            db.Schedule.Remove(schedule);
            db.SaveChanges();

            return Ok(schedule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScheduleExists(int id)
        {
            return db.Schedule.Count(e => e.Id == id) > 0;
        }
    }
}