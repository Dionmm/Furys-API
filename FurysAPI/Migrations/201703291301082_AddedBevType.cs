namespace FurysAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBevType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drinks", "BeverageType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drinks", "BeverageType");
        }
    }
}
