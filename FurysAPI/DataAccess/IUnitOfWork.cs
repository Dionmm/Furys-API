using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurysAPI.DataAccess.Repositories.Interfaces;

namespace FurysAPI.DataAccess
{
    public interface IUnitOfWork: IDisposable
    {
        IBasketContentsRepository BasketContents { get; }
        IDrinkComponentRepository DrinkComponents { get; }
        IDrinkRecipeRepository DrinkRecipes { get; }
        IDrinkRepository Drinks { get; }
        IOrderRepository Orders { get; }

        int Save();
    }
}
