using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sklep.Models
{
    public class SubcategoryModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryID { get; set; }

        public virtual CategoryModel category { get; set; }
    }
}