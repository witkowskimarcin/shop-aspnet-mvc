namespace sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodananowakolumnaprodukt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductModels", "Quantity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductModels", "Quantity");
        }
    }
}
