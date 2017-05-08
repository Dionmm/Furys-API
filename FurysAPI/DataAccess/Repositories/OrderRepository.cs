using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FurysAPI.DataAccess.DataContext;
using FurysAPI.DataAccess.Entities;
using FurysAPI.DataAccess.Repositories.Interfaces;
using FurysAPI.Models;
using FurysAPI.Models.DrinkModels;
using FurysAPI.Models.OrderModels;

namespace FurysAPI.DataAccess.Repositories
{
    public class OrderRepository: Repository<Order>, IOrderRepository
    {
        private readonly IModelFactory _modelFactory;
        public OrderRepository(FurysApiDbContext context) : base(context)
        {
            _modelFactory = new ModelFactory();
        }

        public OrderAdminDetailModel GetOrderDetails(Guid orderId)
        {
            var result = (from o in Context.Orders
                join bc in Context.BasketContents on o.Id equals bc.Order.Id
                join u in Context.Users on o.User.Id equals u.Id
                join d in Context.Drinks on bc.Drink.Id equals d.Id
                where o.Id == orderId
                select new 
                {
                    UserId = u.Id,
                    UserProfile = u.ProfilePhoto,
                    OrderId = o.Id,
                    o.OrderNumber,
                    o.OrderWord,
                    o.Completed,
                    Drink = d
                }
            );

            var drinks = new List<DrinkModel>();
            foreach (var row in result)
            {
                var drinkModel = _modelFactory.Create(row.Drink);
                drinks.Add(drinkModel);
            }
            var single = result.First();
            var model = new OrderAdminDetailModel
            {
                OrderId = single.OrderId,
                UserId = single.UserId,
                UserProfile = single.UserProfile,
                OrderNumber = single.OrderNumber,
                OrderWord = single.OrderWord,
                OrderCompleted = single.Completed,
                Drinks = drinks.ToList()
            };

            return model;

        }
    }
}