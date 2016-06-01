namespace TunashaProjects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeToOrder_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetail", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetail", "Date", c => c.String());
        }
    }
}
