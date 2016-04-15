namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Date = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Post", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.File",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        FilePath = c.String(),
                        DateAdded = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                        Post_PostID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Post", t => t.Post_PostID)
                .Index(t => t.Post_PostID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Date = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reply",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                        QuestionID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Question", t => t.QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.QuestionID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reply", "UserID", "dbo.User");
            DropForeignKey("dbo.Post", "UserID", "dbo.User");
            DropForeignKey("dbo.Reply", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.File", "Post_PostID", "dbo.Post");
            DropForeignKey("dbo.Comment", "PostID", "dbo.Post");
            DropIndex("dbo.Reply", new[] { "UserID" });
            DropIndex("dbo.Reply", new[] { "QuestionID" });
            DropIndex("dbo.Post", new[] { "UserID" });
            DropIndex("dbo.File", new[] { "Post_PostID" });
            DropIndex("dbo.Comment", new[] { "PostID" });
            DropTable("dbo.User");
            DropTable("dbo.Reply");
            DropTable("dbo.Question");
            DropTable("dbo.Post");
            DropTable("dbo.File");
            DropTable("dbo.Comment");
        }
    }
}
