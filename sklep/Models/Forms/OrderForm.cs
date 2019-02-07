using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sklep.Models.Forms
{
    public class OrderForm
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Locality { get; set; }
        public string Street { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public int Shipment { get; set; }
        public bool Remember { get; set; }
    }
}