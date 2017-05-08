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

        public override Task OnConnected()
        {
            try
            {
                GetToken();
                Clients.Caller.displayOrders(GetOrders());
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No token");
                Clients.Caller.error("Server error occurred");
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

        private IEnumerable<OrderAdminMultiModel> GetOrders()
        {
            var orders = UnitOfWork.Orders.GetAll();

            var models = _modelFactory.Create(orders);

            return models;
        }

    }
}