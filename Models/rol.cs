using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blog.Models
{
    public class rol
    {
        public int rolId { get; set; }
        public string roltype { get; set; }
        public virtual kullanici kullanici { get; set; }





    }
}