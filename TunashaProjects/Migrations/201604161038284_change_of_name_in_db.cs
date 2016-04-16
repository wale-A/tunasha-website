namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_of_name_in_db : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.File", newName: "PostedFile");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PostedFile", newName: "File");
        }
    }
}
