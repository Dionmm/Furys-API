using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FurysAPI.DataAccess.Entities;
using FurysAPI.Models.DrinkModels;
using FurysAPI.Models.OrderModels;

namespace FurysAPI.Models
{
    public class ModelFactory: IModelFactory
    {
        public Drink Create(DrinkModel model)
        {
            return new Drink
            {
                Name = model.Name,
                Price = model.Price,
                BeverageType = model.BeverageType
            };
        }

        public DrinkModel Create(Drink drink)
        {
            return new DrinkModel
            {
                Name = drink.Name,
                Price = drink.Price,
                Id = drink.Id,
                BeverageType = drink.BeverageType
            };
        }

        public IEnumerable<DrinkModel> Create(IEnumerable<Drink> drinks)
        {
            return drinks.Select(drink => new DrinkModel
            {
                Name = drink.Name,
                Price = drink.Price,
                Id = drink.Id,
                BeverageType = drink.BeverageType
            });
        }

        //This only needs the Id for now, can implement db lookups for the null values later if needed
        public IEnumerable<Drink> Create(IEnumerable<DrinkModel> models)
        {
            return models.Select(model => new Drink
            {
                Id = model.Id
            });
        }

        public OrderCompleteModel Create(string result, string word, int orderNumber)
        {
            return new OrderCompleteModel
            {
                Result = result,
                OrderNumber = orderNumber,
                OrderWord = word
            };
        }
    }
}