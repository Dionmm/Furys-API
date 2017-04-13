namespace FurysAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeOrderNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "OrderNumber", c => c.String());
        }
    }
}
