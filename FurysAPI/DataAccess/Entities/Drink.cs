using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurysAPI.DataAccess.Entities
{
    public class Drink : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string BeverageType { get; set; }
        public virtual ICollection<DrinkRecipe> DrinkRecipe { get; set; }
        public virtual ICollection<BasketContents> BasketContents { get; set; }
    }
}