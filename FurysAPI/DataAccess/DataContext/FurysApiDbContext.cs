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
        /*
        public virtual DbSet<BannedWord> BannedWords { get; set; }
        public virtual DbSet<ExifData> ExifData { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        */
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