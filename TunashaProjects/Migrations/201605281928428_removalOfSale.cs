namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removalOfSale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.User", "RoleID", c => c.Int(nullable: false));
            CreateIndex("dbo.User", "RoleID");
            AddForeignKey("dbo.User", "RoleID", "dbo.Role", "ID", cascadeDelete: true);
            DropTable("dbo.Sale");
        }
        
        public override void Down()
        {
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
            
            DropForeignKey("dbo.User", "RoleID", "dbo.Role");
            DropIndex("dbo.User", new[] { "RoleID" });
            DropColumn("dbo.User", "RoleID");
            DropTable("dbo.Role");
        }
    }
}
