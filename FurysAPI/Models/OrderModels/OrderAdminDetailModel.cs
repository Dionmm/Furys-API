using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FurysAPI.Models.DrinkModels;

namespace FurysAPI.Models.OrderModels
{
    public class OrderAdminDetailModel
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public int OrderNumber { get; set; }
        public string OrderWord { get; set; }
        public bool OrderCompleted { get; set; }
        public string UserProfile { get; set; }
        public ICollection<DrinkModel> Drinks { get; set; }
    }
}