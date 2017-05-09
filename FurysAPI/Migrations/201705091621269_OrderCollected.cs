namespace FurysAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderCollected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Collected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Collected");
        }
    }
}
