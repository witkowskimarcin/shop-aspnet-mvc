using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sklep.Models
{
    public class ImageModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string image { get; set; }
    }
}