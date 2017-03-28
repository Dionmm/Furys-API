using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FurysAPI.DataAccess.Entities;
using FurysAPI.Models.DrinkModels;

namespace FurysAPI.Models
{
    public class ModelFactory: IModelFactory
    {
        public Drink Create(DrinkModel model)
        {
            return new Drink
            {
                Name = model.Name,
                Price = model.Price
            };
        }

        public DrinkModel Create(Drink drink)
        {
            return new DrinkModel
            {
                Name = drink.Name,
                Price = drink.Price,
                Id = drink.Id
            };
        }

        public IEnumerable<DrinkModel> Create(IEnumerable<Drink> drinks)
        {
            return drinks.Select(drink => new DrinkModel
            {
                Name = drink.Name,
                Price = drink.Price,
                Id = drink.Id
            });
        }
    }
}