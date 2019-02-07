using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sklep.Models
{
    public class UserDetails
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Locality { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}