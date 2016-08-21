//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Description;
//using blog.Models;

//namespace blog.Controllers
//{
//    public class tokenController : ApiController
//    {
//        private blogcontext db = new blogcontext();

//        // GET api/token
//        public IQueryable<token> Gettokens()
//        {
//            return db.tokens;
//        }

//        // GET api/token/5
//        [ResponseType(typeof(token))]
//        public IHttpActionResult Gettoken(int id)
//        {
//            token token = db.tokens.Find(id);
//            if (token == null)
//            {
//                return NotFound();
//            }

//            return Ok(token);
//        }

//        // PUT api/token/5
//        public IHttpActionResult Puttoken(int id, token token)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != token.id)
//            {
//                return BadRequest();
//            }

//            db.Entry(token).State = EntityState.Modified;

//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!tokenExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // POST api/token
//        [ResponseType(typeof(token))]
//        public HttpResponseMessage Post(string ad,string sifre)
//        {
//            var query = from kullanici in db.kullanicilar
//                        where kullanici.kullaniciAd == ad && kullanici.password == sifre
//                        select kullanici;
//            HttpResponseMessage resp;
//            int sayi;
//            sayi = query.Count();
//            if (sayi > 0)
//            {
//                kullanici kul = query.First<kullanici>();
//                if (kul.password == sifre)
//                {
//                    token newtoken = new token();
//                    newtoken.tknaccess = kul.kullaniciAd + kul.DogumYeri ;
//                    newtoken.date = DateTime.Now;
                   
//                    newtoken.kullanici = kul;
                    
//                    db.tokens.Add(newtoken);
//                    db.SaveChanges();
//                    resp = Request.CreateResponse(HttpStatusCode.OK, newtoken.tknaccess);
//                    return resp;

//                }
//                else { resp = Request.CreateResponse(HttpStatusCode.BadRequest); return resp; }

//            }
//            else { resp = Request.CreateResponse(HttpStatusCode.NotFound); return resp; }



//        }

//        // DELETE api/token/5
//        [ResponseType(typeof(token))]
//        public IHttpActionResult Deletetoken(int id)
//        {
//            token token = db.tokens.Find(id);
//            if (token == null)
//            {
//                return NotFound();
//            }

//            db.tokens.Remove(token);
//            db.SaveChanges();

//            return Ok(token);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool tokenExists(int id)
//        {
//            return db.tokens.Count(e => e.id == id) > 0;
//        }
//    }
//}