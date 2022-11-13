namespace WebSSLProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductBrand",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(maxLength: 50, unicode: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Quantity = c.Int(nullable: false),
                        Stock = c.String(maxLength: 50, unicode: false),
                        SalesDate = c.DateTime(nullable: false, storeType: "date"),
                        ProductPhoto = c.String(maxLength: 500, unicode: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductBrand", t => t.BrandId)
                .Index(t => t.BrandId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "BrandId", "dbo.ProductBrand");
            DropIndex("dbo.Product", new[] { "BrandId" });
            DropTable("dbo.Product");
            DropTable("dbo.ProductBrand");
        }
    }
}
