using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FurysAPI.DataAccess.DataContext;
using FurysAPI.DataAccess.Entities;
using FurysAPI.DataAccess.Repositories.Interfaces;

namespace FurysAPI.DataAccess.Repositories
{
    public class OrderRepository: Repository<Order>, IOrderRepository
    {
        public OrderRepository(FurysApiDbContext context) : base(context)
        {
        }
    }
}