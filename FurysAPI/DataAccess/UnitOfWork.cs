using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FurysAPI.DataAccess.DataContext;
using FurysAPI.DataAccess.Repositories;
using FurysAPI.DataAccess.Repositories.Interfaces;

namespace FurysAPI.DataAccess
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly FurysApiDbContext _context;

        public UnitOfWork(FurysApiDbContext context)
        {
            _context = context;
            //Add new Repositories in here
            BasketContents = new BasketContentsRepository(_context);
            DrinkComponents = new DrinkComponentRepository(_context);
            DrinkRecipes = new DrinkRecipeRepository(_context);
            Drinks = new DrinkRepository(_context);
            Orders = new OrderRepository(_context);
        }
        
        public IBasketContentsRepository BasketContents { get; }
        public IDrinkComponentRepository DrinkComponents { get; }
        public IDrinkRecipeRepository DrinkRecipes { get; }
        public IDrinkRepository Drinks { get; }
        public IOrderRepository Orders { get; }
        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}