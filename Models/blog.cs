using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace blog.Models
{
    public class blog
    {
        public int blogId { get; set; }
        [Required]
        public string baslik { get; set; }
        [StringLength(250)]
        public string icerik { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime tarih { get; set; }
        public virtual kullanici kullanici { get; set; }

    }
}