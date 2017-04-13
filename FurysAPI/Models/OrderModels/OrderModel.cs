using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FurysAPI.Models.DrinkModels;

namespace FurysAPI.Models.OrderModels
{
    public class OrderModel
    {
        [Required]
        public IEnumerable<DrinkModel> Drinks { get; set; }
        [Required]
        public string Token { get; set; }
    }
}