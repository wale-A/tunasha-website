namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_of_name_in_db1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "DisableComment", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Post", "DisableComment");
        }
    }
}
