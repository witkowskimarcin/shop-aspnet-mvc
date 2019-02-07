using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sklep.Models
{
    public class ProductModel : IComparable<ProductModel>
    {
        
        [Key]
        public int ID { get; set; }
        [Required]
        public int SubcategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Quantity { get; set; }

        public virtual SubcategoryModel subcategory { get; set; }
        public virtual ICollection<ImageModel> images { get; set; }

        public int CompareTo(ProductModel that)
        {
            if (this.ID > that.ID) return -1;
            if (this.ID == that.ID) return 0;
            return 1;
        }
    }
}