using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FurysAPI.DataAccess.DataContext;
using FurysAPI.DataAccess.Entities;
using FurysAPI.DataAccess.Repositories.Interfaces;

namespace FurysAPI.DataAccess.Repositories
{
    public class DrinkRepository: Repository<Drink>, IDrinkRepository
    {
        public DrinkRepository(FurysApiDbContext context) : base(context)
        {
        }
        
        /* Takes a beverage type (Beer, Spirit, etc.) a page size (Number of results to return)
         * and the page start point. From here it calculates how many records to skip and how
         * many to return. i.e. page 2 with pageSize 25 would skip the first 50 records and return
         * rows 51 to 76
         */
        public IEnumerable<Drink> GetByBeverageType(string beverageType, int pageSize, int page)
        {
            var skipRows = page * pageSize;
            return Context.Drinks.OrderByDescending(x => x.Name).Where(x => x.BeverageType == beverageType).Skip(skipRows).Take(pageSize).ToList();
        }
    }
}