using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sklep.Controllers
{
    public class ProductAddForm
    {
        public int SubcategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }

        public HttpPostedFileBase[] files { get; set; }
    }
}