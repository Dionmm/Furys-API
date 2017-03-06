using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurysAPI.DataAccess.Entities
{
    public class BasketContents : BaseEntity
    {
        public virtual Drink Drink { get; set; }
        public virtual Order Order { get; set; }
    }
}