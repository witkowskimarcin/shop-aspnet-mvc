using sklep.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace sklep.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("DefaultConnection")
        {

        }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<SubcategoryModel> Subcategory { get; set; }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<ImageModel> Image { get; set; }
        public DbSet<OrderDetailModel> OrderDetail { get; set; }
        public DbSet<OrderModel> Order { get; set; }
        public DbSet<PromotedProducts> PromotedProducts { get; set; }
        public DbSet<MainPromotion> MainPromotion { get; set; }
    }
}