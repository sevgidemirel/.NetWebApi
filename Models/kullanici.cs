using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace blog.Models
{
    public class kullanici
    {

     
        public int kullaniciId { get; set; }
        [Required]
        public string kullaniciAd { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime DogumTarihi { get; set; }
        public string DogumYeri { get; set; }
        public virtual ICollection<blog> bloglar { get; set; }
        //[Required]
        [DataType(DataType.Password)]
        public string password { get; set; }




    }

    
}