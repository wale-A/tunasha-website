namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class store_Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetail", "TransactionNumber", c => c.String());
            AddColumn("dbo.OrderDetail", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetail", "UserID");
            CreateIndex("dbo.Order", "UserID");
            AddForeignKey("dbo.OrderDetail", "UserID", "dbo.User", "UserID", cascadeDelete: false);
            AddForeignKey("dbo.Order", "UserID", "dbo.User", "UserID", cascadeDelete: false);
            DropColumn("dbo.Order", "TransactionNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "TransactionNumber", c => c.String());
            DropForeignKey("dbo.Order", "UserID", "dbo.User");
            DropForeignKey("dbo.OrderDetail", "UserID", "dbo.User");
            DropIndex("dbo.Order", new[] { "UserID" });
            DropIndex("dbo.OrderDetail", new[] { "UserID" });
            DropColumn("dbo.Order", "UserID");
            DropColumn("dbo.OrderDetail", "UserID");
            DropColumn("dbo.OrderDetail", "TransactionNumber");
        }
    }
}
