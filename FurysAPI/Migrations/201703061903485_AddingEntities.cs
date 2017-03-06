namespace FurysAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketContents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UpdatedDateTime = c.DateTime(nullable: false),
                        Drink_Id = c.Guid(),
                        Order_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drinks", t => t.Drink_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Drink_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Drinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UpdatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DrinkRecipes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UpdatedDateTime = c.DateTime(nullable: false),
                        Drink_Id = c.Guid(),
                        DrinkComponents_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drinks", t => t.Drink_Id)
                .ForeignKey("dbo.DrinkComponents", t => t.DrinkComponents_Id)
                .Index(t => t.Drink_Id)
                .Index(t => t.DrinkComponents_Id);
            
            CreateTable(
                "dbo.DrinkComponents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UpdatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContainsFavourite = c.Boolean(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        OrderCompletedTime = c.DateTime(nullable: false),
                        OrderNumber = c.String(),
                        OrderWord = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UpdatedDateTime = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Users", "FirstName", c => c.String());
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "Birthday", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "ProfilePhoto", c => c.String());
            AddColumn("dbo.Users", "Online", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Gender", c => c.String());
            AddColumn("dbo.Users", "LocationEnabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.Users");
            DropForeignKey("dbo.BasketContents", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.DrinkRecipes", "DrinkComponents_Id", "dbo.DrinkComponents");
            DropForeignKey("dbo.DrinkRecipes", "Drink_Id", "dbo.Drinks");
            DropForeignKey("dbo.BasketContents", "Drink_Id", "dbo.Drinks");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.DrinkRecipes", new[] { "DrinkComponents_Id" });
            DropIndex("dbo.DrinkRecipes", new[] { "Drink_Id" });
            DropIndex("dbo.BasketContents", new[] { "Order_Id" });
            DropIndex("dbo.BasketContents", new[] { "Drink_Id" });
            DropColumn("dbo.Users", "LocationEnabled");
            DropColumn("dbo.Users", "Gender");
            DropColumn("dbo.Users", "Online");
            DropColumn("dbo.Users", "ProfilePhoto");
            DropColumn("dbo.Users", "Birthday");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
            DropTable("dbo.Orders");
            DropTable("dbo.DrinkComponents");
            DropTable("dbo.DrinkRecipes");
            DropTable("dbo.Drinks");
            DropTable("dbo.BasketContents");
        }
    }
}
