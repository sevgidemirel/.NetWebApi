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
using blog.Models;

namespace blog.Controllers
{
    public class rolsController : ApiController
    {
        private blogcontext db = new blogcontext();

        // GET: api/rols
        public List<rol> Getrols()
        {
            var rol = db.rols.ToList();
            return rol;
        }

        // GET: api/rols/5
        [ResponseType(typeof(rol))]
        public IHttpActionResult Getrol(int id)
        {
            rol rol = db.rols.Find(id);
            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }

        // PUT: api/rols/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putrol(int id, rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rol.rolId)
            {
                return BadRequest();
            }

            db.Entry(rol).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!rolExists(id))
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

        // POST: api/rols
        [ResponseType(typeof(rol))]
        public IHttpActionResult Postrol(rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.rols.Add(rol);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rol.rolId }, rol);
        }

        // DELETE: api/rols/5
        [ResponseType(typeof(rol))]
        public IHttpActionResult Deleterol(int id)
        {
            rol rol = db.rols.Find(id);
            if (rol == null)
            {
                return NotFound();
            }

            db.rols.Remove(rol);
            db.SaveChanges();

            return Ok(rol);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool rolExists(int id)
        {
            return db.rols.Count(e => e.rolId == id) > 0;
        }
    }
}