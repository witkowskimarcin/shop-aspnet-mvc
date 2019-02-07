using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sklep.Models
{
    public class OrderModel
    {

        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
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
        public string Phone { get; set; }
        [Required]
        public int Shipment { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public virtual ICollection<OrderDetailModel> orderDetails { get; set; }
        public virtual UserDetails user { get; set; }
    }
}