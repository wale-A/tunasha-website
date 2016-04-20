namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeInProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "PostedFileID", c => c.Int(nullable: false));
            CreateIndex("dbo.Product", "PostedFileID");
            AddForeignKey("dbo.Product", "PostedFileID", "dbo.PostedFile", "ID", cascadeDelete: true);
            DropColumn("dbo.Product", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "ImageUrl", c => c.String());
            DropForeignKey("dbo.Product", "PostedFileID", "dbo.PostedFile");
            DropIndex("dbo.Product", new[] { "PostedFileID" });
            DropColumn("dbo.Product", "PostedFileID");
        }
    }
}
