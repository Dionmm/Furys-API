using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using FurysAPI.DataAccess.Entities;
using FurysAPI.Models.DrinkModels;
using FurysAPI.Models.OrderModels;

namespace FurysAPI.Models
{
    interface IModelFactory
    {
        Drink Create(DrinkModel model);
        DrinkModel Create(Drink drink);
        IEnumerable<DrinkModel> Create(IEnumerable<Drink> drinks);
        IEnumerable<Drink> Create(IEnumerable<DrinkModel> models);
        OrderCompleteModel Create(string result, string word, int orderNumber);
        IEnumerable<OrderAdminMultiModel> Create(IEnumerable<Order> orders);

        OrderAdminDetailModel Create(IQueryable orderDetails);

    }
}
