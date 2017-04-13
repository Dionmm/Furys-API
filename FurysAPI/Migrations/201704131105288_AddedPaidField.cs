namespace FurysAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPaidField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Paid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Paid");
        }
    }
}
