using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurysAPI.DataAccess.Entities
{
    public class DrinkRecipe : BaseEntity
    {
        public virtual Drink Drink { get; set; }
        public virtual DrinkComponent DrinkComponents { get; set; }
    }
}