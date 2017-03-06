using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using FurysAPI.DataAccess.Entities;

namespace FurysAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FurysAPI.DataAccess.DataContext.FurysApiDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FurysAPI.DataAccess.DataContext.FurysApiDbContext context)
        {
            var dion = new User
            {
                UserName = "Dionmm",
                Email = "dion@macinty.re",
                PhoneNumber = "07881913156",
                FirstName = "Dion",
                LastName = "MacIntyre",
                Birthday = Convert.ToDateTime("23/04/1994"),
                Online = false,
                LocationEnabled = false
            };
            var jack = new User
            {
                UserName = "JackBlack",
                Email = "jack@macinty.re",
                PhoneNumber = "07881913134",
                Birthday = Convert.ToDateTime("13/10/1994"),
                Online = false,
                LocationEnabled = false
            };

            var drink1 = new Drink
            {
                Id = Guid.NewGuid(),
                Name = "Jack Daniels Coke",
                Price = 3.5m,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,

            };
            var drink2 = new Drink
            {
                Id = Guid.NewGuid(),
                Name = "Vodka Lemonade",
                Price = 2,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            };
            var drink3 = new Drink
            {
                Id = Guid.NewGuid(),
                Name = "Vodka Blackcurrent Lime",
                Price = 2,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            };
            var drink4 = new Drink
            {
                Id = Guid.NewGuid(),
                Name = "Vodka Irn Bru",
                Price = 2,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            };
            var drink5 = new Drink
            {
                Id = Guid.NewGuid(),
                Name = "Southern Comfort Lemonade",
                Price = 2,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            };
            var drink6 = new Drink
            {
                Id = Guid.NewGuid(),
                Name = "Rum Coke",
                Price = 2,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            };
            var drink7 = new Drink
            {
                Id = Guid.NewGuid(),
                Name = "Beer",
                Price = 2,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            };

            var component1 = new DrinkComponent
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Vodka"
            };
            var component2 = new DrinkComponent
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Southern Comfort"
            };
            var component3 = new DrinkComponent
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Lemonade"
            };
            var component4 = new DrinkComponent
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Coke"
            };
            var component5 = new DrinkComponent
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Rum"
            };
            var component6 = new DrinkComponent
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Jack Daniels"
            };
            var component7 = new DrinkComponent
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Irn Bru"
            };

            var recipe1 = new DrinkRecipe
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Drink = drink2,
                DrinkComponents = component1,
                Id = Guid.NewGuid()
            };
            var recipe2 = new DrinkRecipe
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Drink = drink2,
                DrinkComponents = component3,
                Id = Guid.NewGuid()
            };

            var order1 = new Order
            {
                Completed = false,
                ContainsFavourite = false,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                OrderCompletedTime = DateTime.Now,
                OrderNumber = "33",
                OrderWord = "Explorer",
                TotalCost = 6,
                User = jack
            };

            var contents1 = new BasketContents
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Drink = drink1,
                Id = Guid.NewGuid(),
                Order = order1
            };
            var contents2 = new BasketContents
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Drink = drink1,
                Id = Guid.NewGuid(),
                Order = order1
            };
            var contents3 = new BasketContents
            {
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Drink = drink1,
                Id = Guid.NewGuid(),
                Order = order1
            };

            /*context.Users.AddOrUpdate(dion);
            context.Users.AddOrUpdate(jack); //Migration breaks when AddorUpdate a user: Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.

            context.Drinks.AddOrUpdate(drink1);
            context.Drinks.AddOrUpdate(drink2);
            context.Drinks.AddOrUpdate(drink3);
            context.Drinks.AddOrUpdate(drink4);
            context.Drinks.AddOrUpdate(drink5);
            context.Drinks.AddOrUpdate(drink6);
            context.Drinks.AddOrUpdate(drink7);

            context.DrinkComponents.AddOrUpdate(component1);
            context.DrinkComponents.AddOrUpdate(component2);
            context.DrinkComponents.AddOrUpdate(component3);
            context.DrinkComponents.AddOrUpdate(component4);
            context.DrinkComponents.AddOrUpdate(component5);
            context.DrinkComponents.AddOrUpdate(component6);
            context.DrinkComponents.AddOrUpdate(component7);

            context.DrinkRecipes.AddOrUpdate(recipe1);
            context.DrinkRecipes.AddOrUpdate(recipe2);

            context.Orders.AddOrUpdate(order1);

            context.BasketContents.AddOrUpdate(contents1);
            context.BasketContents.AddOrUpdate(contents2);
            context.BasketContents.AddOrUpdate(contents3);*/
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
