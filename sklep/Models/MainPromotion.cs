using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sklep.Models
{
    public class MainPromotion
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int ProductID { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public int Left { get; set; }
        public virtual ProductModel product { get; set; }
    }
}