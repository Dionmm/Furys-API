using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurysAPI.DataAccess.Entities
{
    public class DrinkComponent : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual ICollection<DrinkRecipe> DrinkRecipe { get; set; }
    }
}