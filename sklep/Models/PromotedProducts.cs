using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sklep.Models
{
    public class PromotedProducts
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int ProductID { get; set; }
        public virtual ProductModel product { get; set; }
    }
}