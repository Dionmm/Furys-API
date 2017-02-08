using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FurysAPI.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public virtual string FirstName { get; set; }
        public virtual string  LastName { get; set; }
        public virtual DateTime Birthday { get; set; }
        public virtual string ProfilePhoto { get; set; }
        public virtual bool Online { get; set; }
        public virtual string Gender { get; set; }
        public virtual bool LocationEnabled { get; set; }
          
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}