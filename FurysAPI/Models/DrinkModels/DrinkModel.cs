using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FurysAPI.Models.DrinkModels
{
    public class DrinkModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string BeverageType { get; set; }
    }
}