using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using FurysAPI.App_Start;
using FurysAPI.DataAccess;
using FurysAPI.DataAccess.DataContext;
using FurysAPI.DataAccess.Entities;
using FurysAPI.Models;
using FurysAPI.Models.OrderModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;

namespace FurysAPI.Hubs
{

    public class OrderHub : Hub
    {

        private ApplicationUserManager _userManager;
        private readonly FurysApiDbContext _context = new FurysApiDbContext();
        private IUnitOfWork _unitOfWork;
        private IModelFactory _modelFactory = new ModelFactory();

        private ApplicationUserManager UserManager
        {
            get { return _userManager ?? new ApplicationUserManager(new UserStore<User>(_context)); }
            set { _userManager = value; }
        }

        private IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork ?? new UnitOfWork(_context); }
            set { _unitOfWork = value; }
        }

        private User CurrentUser { get; set; }

        public override Task OnConnected()
        {
            try
            {
                GetToken();
                AddToGroup();
                Clients.All.userConnected(CurrentUser.UserName + " Joined");
                Clients.Caller.displayOrders(GetOrders());
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No token");
                Clients.Caller.error("Server error occurred 1");
            }

            return base.OnConnected();
        }

        public void OrderDetails(Guid id)
        {
            try
            {
                Clients.Caller.orderDetails("testingas "+ id);

                var order = UnitOfWork.Orders.GetOrderDetails(id);
                

                Clients.Caller.orderDetails(order);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No username");
                Clients.Caller.error("Username not found");
            }
        }

        public void NewOrder(Guid id)
        {
            try
            {
                var order = UnitOfWork.Orders.Get(id);
                var model = _modelFactory.Create(order);
                Clients.Group("JackBlack").newOrder(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("No order");
                Clients.Caller.error("Server error occured when finding order");
            }
        }

        public void OrderComplete(Guid id)
        {
            try
            {
                var order = UnitOfWork.Orders.Get(id);
                order.Completed = true;
                order.OrderCompletedTime = DateTime.Now;
                order.UpdatedDateTime = DateTime.Now;
         
                

                if (UnitOfWork.Save() == 0)
                {
                    throw new Exception();
                }
                var response = new Dictionary<string, string>
                {
                    {"success", "true"},
                    {"orderId", order.Id.ToString() }
                };
                Clients.Group(order.User.UserName).orderComplete(response);
                Clients.Group("JackBlack").orderComplete(response);
            }
            catch (Exception)
            {
                Clients.Caller.error("Server error occured");
            }
        }

        public void OrderCollected(Guid id)
        {
            try
            {
                var order = UnitOfWork.Orders.Get(id);
                order.Collected = true;
                order.UpdatedDateTime = DateTime.Now;

                if (UnitOfWork.Save() == 0)
                {
                    throw new Exception();
                }
                var response = new Dictionary<string, string>
                {
                    {"success", "true"},
                    {"orderId", order.Id.ToString() }
                };
                Clients.Group(order.User.UserName).orderCollected(response);
                Clients.Group("JackBlack").orderCollected(response);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No username");
                Clients.Caller.error("Server error occured");
            }
        }

        //The auth token needs to be sent as a query string with the initial connection request
        //this then grabs it and creates a user object to be used as authorisation
        private void GetToken()
        {
            var token = Context.Request.QueryString.Get("bearerToken");
            if (token != null && token != "undefined")
            {
                var tk = Startup.OAuthOptions.AccessTokenFormat.Unprotect(token);
                var principal = new ClaimsPrincipal(tk.Identity);
                SetUser(principal);
            }
            else
            {
                Clients.Caller.error("No token");
            }

        }

        private void SetUser(ClaimsPrincipal user)
        {
            var username = user.Identity.Name;
            if (username != null)
            {
                CurrentUser = UserManager.FindByName(username);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No username");
                Clients.Caller.error("Username not found");
            }
        }

        private IEnumerable<OrderAdminMultiModel> GetOrders()
        {
            var orders = UnitOfWork.Orders.GetUncollectedOrders();

            var models = _modelFactory.Create(orders);

            return models;
        }

        private void AddToGroup()
        {
            Groups.Add(Context.ConnectionId, CurrentUser.UserName);
        }

    }
}