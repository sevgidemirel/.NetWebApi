using blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel.DataAnnotations;
using JsonPatch;
using System.Web.Http;
using System.Net.Http;



namespace blog.Controllers
{
    [Authorize]
    //[HttpGet]
    public class kullaniciController : ApiController
    {
        blogcontext db = new blogcontext();

        [Authorize(Roles = "junioradmin")]
        public List<kullanici> Get()
        {
            var kullanici = db.kullanicilar.ToList<kullanici>();
            return kullanici;
        }
        [Authorize(Roles = "admin")]
        public HttpResponseMessage Get(int id)
        {
            var kullanici = db.kullanicilar.Find(id);
            HttpResponseMessage msg;

            if (kullanici == null)
            {
                msg = Request.CreateResponse(HttpStatusCode.BadRequest, "tanımlanan id bulunamadı.");
                return msg;
            }
            else
            {
                msg = Request.CreateResponse(HttpStatusCode.Found, kullanici);
                return msg;
            }
        }

        public HttpResponseMessage Get(int index, int size)
        {
            HttpResponseMessage resp = Request.CreateResponse(HttpStatusCode.OK, db.kullanicilar.OrderBy(q => q.kullaniciId).Skip((index - 1) * size).Take(size));

            return resp;

        } //kontrol et.

        public HttpResponseMessage Get(string ad, string sifre)
        {
            var query = from kullanici in db.kullanicilar
                        where
                            kullanici.kullaniciAd == ad && kullanici.password == sifre
                        select kullanici;
            HttpResponseMessage resp;
            int sayi;
            sayi = query.Count();
            if (sayi > 0)
            {
                resp = Request.CreateResponse(HttpStatusCode.OK, db.kullanicilar.ToList());
                return resp;


            }
            else { resp = Request.CreateResponse(HttpStatusCode.BadRequest, "kul veya sifre yanlis"); return resp; }

        }

        public HttpResponseMessage Get(string tkn)
        {
            var query = from token in db.tokens where token.tknaccess == tkn select token;
            HttpResponseMessage resp;
            int sayi;
            sayi = query.Count();

            if (sayi > 0)
            {
                var list = db.kullanicilar.ToList<kullanici>();
                resp = Request.CreateResponse(HttpStatusCode.OK, list);
                return resp;
            }
            else
            {
                resp = Request.CreateResponse(HttpStatusCode.BadRequest, "deneme");
                return resp;
            }


        }

        //public HttpResponseMessage Get(string name)// !!!!!!!!!!!!!  kontrol döngüleri ekle  !!!!!!!!!!
        //{
        //    var query = from kullanici in db.kullanicilar
        //                where
        //                    kullanici.kullaniciAd == name
        //                select kullanici;
        //    HttpResponseMessage resp;
        //    resp = Request.CreateResponse(HttpStatusCode.Found, query.ToList());
        //        return resp;


        //}

        public HttpResponseMessage Post(kullanici k)
        {
            HttpResponseMessage resp;
            if (!ModelState.IsValid)
            {
                resp = Request.CreateResponse(HttpStatusCode.BadRequest);
                return resp;
            }
            else
            {
                var snc = from kullanici in db.kullanicilar
                          where kullanici.kullaniciAd == k.kullaniciAd
                          select kullanici;
                if (snc.Count() > 0)
                {

                    resp = Request.CreateResponse(HttpStatusCode.BadRequest, "kullanıcı adı önceden alınmış");
                    return resp;
                }
                else
                {
                    k = db.kullanicilar.Add(k);
                    db.SaveChanges();
                    resp = Request.CreateResponse(HttpStatusCode.OK, "vt'ye eklendi");
                    return resp;
                }
            }
        }
        //public IHttpActionResult Post(kullanici kul)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    { 
        //         var snc = from kullanici in db.kullanicilar
        //                 where kullanici.kullaniciAd == kul.kullaniciAd
        //                 select kullanici;
        //         if (snc.Count() > 0) {
        //             return BadRequest("kullanıcı adı önceden alınmış");
        //         }
        //         else
        //         {
        //             kul = db.kullanicilar.Add(kul);
        //             db.SaveChanges();
        //             return Ok();
        //         }
        //    }
        
            


        



        public HttpResponseMessage Put(int id, kullanici k)
        {
            HttpResponseMessage resp;
            var kul = db.kullanicilar.Find(id);
            if (ModelState.IsValid && kul != null) // K ?
            {
                k.kullaniciId = id;
                db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                resp = Request.CreateResponse(HttpStatusCode.OK);
                return resp;
            }
            else
            {
                resp = Request.CreateResponse(HttpStatusCode.BadRequest);
                return resp;
            }
        }

        public HttpResponseMessage Patch(Guid id, JsonPatchDocument<kullanici> patch)
        {
            kullanici upt = db.kullanicilar.Find(id);
            patch.ApplyUpdatesTo(upt);
            db.Entry(upt).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            HttpResponseMessage resp = Request.CreateResponse(HttpStatusCode.OK);
            return resp;
        }
  

        //[Authorize(Roles="junioradmin")]

        public HttpResponseMessage Delete(int id)
        {
            kullanici kul = db.kullanicilar.Find(id);
            HttpResponseMessage resp;
            if (kul != null)
            {
                db.kullanicilar.Remove(kul);
                db.SaveChanges();
                resp = Request.CreateResponse(HttpStatusCode.OK, "kullani silindi");
                return resp;
            }
            else
            {
                resp = Request.CreateResponse(HttpStatusCode.NotFound);
                return resp;
            }
            //        public void Delete(int id)
            //        {
            //            kullanici kul = db.kullanicilar.Find(id);
            //            if (kul != null)
            //            {
            //                db.kullanicilar.Remove(kul);
            //                db.SaveChanges();
            //            }
            //        }

            //kullanici kullanici = db.kullanicilar.Find(id);
            //if(kullanici!=null)
            //{
            //db.kullanicilar.Remove(kullanici);
            //db.SaveChanges();
            //return Ok();
            //}
            //else { return BadRequest(); }


        }



    }
}


//namespace blog.Controllers
//{
//    public class EmployeeController : ApiController
//    {
//        blogcontext db = new blogcontext();
       
//        public List<kullanici> Get()
//        {
//            var kullanici = db.kullanicilar.ToList<kullanici>();

//            return kullanici; 
//        }

//        public kullanici Get(int id)
//        {
//            return db.kullanicilar.Find(id);
//        }
   

//        public kullanici Post(kullanici kul)
//        {
//            kul = db.kullanicilar.Add(kul);
//            db.SaveChanges();
//            return kul;
//        }

//        public void Delete(int id)
//        {
//            kullanici kul = db.kullanicilar.Find(id);
//            if (kul != null)
//            {
//                db.kullanicilar.Remove(kul);
//                db.SaveChanges();
//            }
//        }

//        public void Put(int id, kullanici kul)
//        {
//            kul.kullaniciId = id;
//            db.Entry(kul).State = System.Data.Entity.EntityState.Modified;
//            db.SaveChanges();
//        }

//    }
//}

