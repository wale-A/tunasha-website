namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class store_Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "UserID");
            AddForeignKey("dbo.Order", "UserID", "dbo.User", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "UserID", "dbo.User");
            DropIndex("dbo.Order", new[] { "UserID" });
            DropColumn("dbo.Order", "UserID");
        }
    }
}
