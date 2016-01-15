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
    public class FollowersController : ApiController
    {
        private TimeViewContext db = new TimeViewContext();

        // GET: api/Followers
        public IQueryable<Follower> GetFollower()
        {
            return db.Follower;
        }

        // GET: api/Followers/5
        [ResponseType(typeof(Follower))]
        public IHttpActionResult GetFollower(int id)
        {
            Follower follower = db.Follower.Find(id);
            if (follower == null)
            {
                return NotFound();
            }

            return Ok(follower);
        }

        // PUT: api/Followers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFollower(int id, Follower follower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != follower.Id)
            {
                return BadRequest();
            }

            db.Entry(follower).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowerExists(id))
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

        // POST: api/Followers
        [ResponseType(typeof(Follower))]
        public IHttpActionResult PostFollower(Follower follower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Follower.Add(follower);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = follower.Id }, follower);
        }

        // DELETE: api/Followers/5
        [ResponseType(typeof(Follower))]
        public IHttpActionResult DeleteFollower(int id)
        {
            Follower follower = db.Follower.Find(id);
            if (follower == null)
            {
                return NotFound();
            }

            db.Follower.Remove(follower);
            db.SaveChanges();

            return Ok(follower);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FollowerExists(int id)
        {
            return db.Follower.Count(e => e.Id == id) > 0;
        }
    }
}