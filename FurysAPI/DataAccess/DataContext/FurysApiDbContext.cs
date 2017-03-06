using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using FurysAPI.DataAccess.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FurysAPI.DataAccess.DataContext
{
    public class FurysApiDbContext : IdentityDbContext<User>
    {
        public FurysApiDbContext() : base("DefaultConnection")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
        
        public virtual DbSet<BasketContents> BasketContents { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<DrinkComponent> DrinkComponents { get; set; }
        public virtual DbSet<DrinkRecipe> DrinkRecipes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        
        public static FurysApiDbContext Create()
        {
            return new FurysApiDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Change some table names in the DB to something more sensible
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

        }
    }
}