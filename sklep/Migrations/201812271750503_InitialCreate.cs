namespace sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SubcategoryModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryModels", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        image = c.String(nullable: false),
                        ProductModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductModels", t => t.ProductModel_ID)
                .Index(t => t.ProductModel_ID);
            
            CreateTable(
                "dbo.MainPromotions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        Code = c.String(),
                        Quantity = c.Int(nullable: false),
                        Left = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductModels", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubcategoryID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SubcategoryModels", t => t.SubcategoryID, cascadeDelete: true)
                .Index(t => t.SubcategoryID);
            
            CreateTable(
                "dbo.OrderModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Locality = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        Zipcode = c.String(nullable: false),
                        Phone = c.String(),
                        Shipment = c.Int(nullable: false),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserDetails", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.OrderDetailModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OrderModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductModels", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.OrderModels", t => t.OrderModel_ID)
                .Index(t => t.ProductID)
                .Index(t => t.OrderModel_ID);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Locality = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        Zipcode = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PromotedProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductModels", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PromotedProducts", "ProductID", "dbo.ProductModels");
            DropForeignKey("dbo.OrderModels", "UserID", "dbo.UserDetails");
            DropForeignKey("dbo.OrderDetailModels", "OrderModel_ID", "dbo.OrderModels");
            DropForeignKey("dbo.OrderDetailModels", "ProductID", "dbo.ProductModels");
            DropForeignKey("dbo.MainPromotions", "ProductID", "dbo.ProductModels");
            DropForeignKey("dbo.ProductModels", "SubcategoryID", "dbo.SubcategoryModels");
            DropForeignKey("dbo.ImageModels", "ProductModel_ID", "dbo.ProductModels");
            DropForeignKey("dbo.SubcategoryModels", "CategoryID", "dbo.CategoryModels");
            DropIndex("dbo.PromotedProducts", new[] { "ProductID" });
            DropIndex("dbo.OrderDetailModels", new[] { "OrderModel_ID" });
            DropIndex("dbo.OrderDetailModels", new[] { "ProductID" });
            DropIndex("dbo.OrderModels", new[] { "UserID" });
            DropIndex("dbo.ProductModels", new[] { "SubcategoryID" });
            DropIndex("dbo.MainPromotions", new[] { "ProductID" });
            DropIndex("dbo.ImageModels", new[] { "ProductModel_ID" });
            DropIndex("dbo.SubcategoryModels", new[] { "CategoryID" });
            DropTable("dbo.PromotedProducts");
            DropTable("dbo.UserDetails");
            DropTable("dbo.OrderDetailModels");
            DropTable("dbo.OrderModels");
            DropTable("dbo.ProductModels");
            DropTable("dbo.MainPromotions");
            DropTable("dbo.ImageModels");
            DropTable("dbo.SubcategoryModels");
            DropTable("dbo.CategoryModels");
        }
    }
}
