using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using FurysAPI.DataAccess.Entities;
using FurysAPI.Models.DrinkModels;

namespace FurysAPI.Models
{
    interface IModelFactory
    {
        Drink Create(DrinkModel model);
        DrinkModel Create(Drink drink);
        IEnumerable<DrinkModel> Create(IEnumerable<Drink> drinks);
        IEnumerable<Drink> Create(IEnumerable<DrinkModel> models);

    }
}
