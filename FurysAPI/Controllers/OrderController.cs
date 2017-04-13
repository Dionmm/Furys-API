using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FurysAPI.App_Start;
using FurysAPI.DataAccess;
using FurysAPI.DataAccess.DataContext;
using FurysAPI.DataAccess.Entities;
using FurysAPI.Models;
using FurysAPI.Models.DrinkModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FurysAPI.Controllers
{
    [RoutePrefix("Order")]
    [Authorize]
    public class OrderController : ApiController
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;
        private FurysApiDbContext _context;
        private readonly IModelFactory _modelFactory;


        public OrderController()
        {
            _modelFactory = new ModelFactory();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public FurysApiDbContext Context
        {
            get
            {
                return _context ?? Request.GetOwinContext().Request.Context.Get<FurysApiDbContext>();
            }
            private set
            {
                _context = value;
            }
        }
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork ?? new UnitOfWork(Context);
            }
            private set
            {
                _unitOfWork = value;
            }
        }

        [HttpPost]
        public IHttpActionResult CreateOrder(IEnumerable<DrinkModel> models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User currentUser = UserManager.FindById(User.Identity.GetUserId());

            if (currentUser == null)
            {
                return InternalServerError();
            }


            List<Drink> drinks = new List<Drink>();
            drinks.AddRange(models.Select(model => UnitOfWork.Drinks.Get(model.Id)));
            if (drinks.Contains(null))
            {
                return BadRequest();
            }

            //Only way i can think to store OrderCompletedTime before it is completed is to
            //set it to max value, anything greater than todays date is not complete. 
            //Cant use nulls in datetime type
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                OrderCompletedTime = DateTime.MaxValue,
                OrderNumber = 1,
                OrderWord = "Humorous",
                TotalCost = drinks.Sum(drink => drink.Price),
                Paid = false,
                Completed = false,
                User = currentUser
            };
            UnitOfWork.Orders.Add(order);

            List<BasketContents> basket = new List<BasketContents>();
            basket.AddRange(drinks.Select(drink => new BasketContents
            {
                Id = Guid.NewGuid(), Drink = drink, Order = order, CreatedDateTime = DateTime.Now, UpdatedDateTime = DateTime.Now
            }));

            UnitOfWork.BasketContents.AddRange(basket);

            if (UnitOfWork.Save() == 0)
            {
                return InternalServerError();
            }

            return Ok(order.Id);
        }


    }
}
