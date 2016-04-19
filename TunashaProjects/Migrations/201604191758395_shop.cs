namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CustomerMail = c.String(),
                        Quantity = c.Int(nullable: false),
                        CustomerPhone = c.String(),
                        ProductID = c.Int(nullable: false),
                        Date = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sale");
            DropTable("dbo.Product");
        }
    }
}
