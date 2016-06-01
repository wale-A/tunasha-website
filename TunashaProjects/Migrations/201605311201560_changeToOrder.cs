namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CustomerAddress2", c => c.String());
            AddColumn("dbo.Order", "CustomerPhone2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "CustomerPhone2");
            DropColumn("dbo.Order", "CustomerAddress2");
        }
    }
}
