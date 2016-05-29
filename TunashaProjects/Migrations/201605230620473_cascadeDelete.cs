namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostedFile", "Post_PostID", "dbo.Post");
            AddForeignKey("dbo.PostedFile", "Post_PostID", "dbo.Post", "PostID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostedFile", "Post_PostID", "dbo.Post");
            AddForeignKey("dbo.PostedFile", "Post_PostID", "dbo.Post", "PostID");
        }
    }
}
