using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurysAPI.DataAccess.Entities;
using FurysAPI.Models.OrderModels;

namespace FurysAPI.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository: IRepository<Order>
    {
        OrderAdminDetailModel GetOrderDetails(Guid orderId);
        IEnumerable<Order> GetUncollectedOrders();
        Order GetPreviousOrder();
    }
}
