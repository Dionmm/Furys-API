using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FurysAPI.Models.OrderModels
{
    public class OrderCompleteModel
    {
        [Required]
        public string Result { get; set; }
        [Required]
        public string OrderWord { get; set; }
        [Required]
        public int OrderNumber { get; set; }

    }
}