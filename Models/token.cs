using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace blog.Models
{
    public class token
    {
        public int id { get; set; }
        public virtual kullanici kullanici { get; set; }
        public string tknaccess { get; set; }
        public DateTime date { get; set; }
    }
}