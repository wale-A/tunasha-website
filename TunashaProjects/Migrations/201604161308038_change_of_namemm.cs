namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_of_namemm : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comment", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comment", "Email", c => c.String(nullable: false));
        }
    }
}
