using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurysAPI.DataAccess.Entities
{
    public class Order : BaseEntity
    {
        public virtual decimal TotalCost { get; set; }
        public virtual bool ContainsFavourite { get; set; }
        public virtual bool Paid { get; set; }
        public virtual bool Completed { get; set; } //If the order has been successfully fulfilled by the bar staff
        public virtual DateTime OrderCompletedTime { get; set; }
        public virtual int OrderNumber { get; set; }
        public virtual string OrderWord { get; set; } //The random word of the day that was active when this order was submitted
        public virtual ICollection<BasketContents> BasketContents { get; set; }
        public virtual User User { get; set; }
    }
}