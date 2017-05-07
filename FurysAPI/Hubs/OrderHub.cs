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

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? new ApplicationUserManager(new UserStore<User>(_context)); }
            set { _userManager = value; }
        }

        public User CurrentUser { get; set; }

        public void GetToken()
        {
            try
            {
                var token = Context.Request.QueryString.Get("bearerToken");
                if (token != null && token != "undefined")
                {
                    var tk = Startup.OAuthOptions.AccessTokenFormat.Unprotect(token);
                    var principal = new ClaimsPrincipal(tk.Identity);
                    SetUser(principal);
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("No token");
                Clients.Caller.error("Server error occurred");
            }
        }

        public void SetUser(ClaimsPrincipal user)
        {
            var username = user.Identity.Name;
            if (username != null)
            {
                _unitOfWork = new UnitOfWork(_context);
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
            GetToken();
            Clients.Caller.test(CurrentUser.FirstName + " logged in.");
            return base.OnConnected();
        }


        public void Hello()
        {
            Clients.All.hello("hello");
        }
    }
}