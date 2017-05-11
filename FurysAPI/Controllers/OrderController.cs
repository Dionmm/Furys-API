using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using FurysAPI.Models.OrderModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Stripe;

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
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public FurysApiDbContext Context
        {
            get { return _context ?? Request.GetOwinContext().Request.Context.Get<FurysApiDbContext>(); }
            private set { _context = value; }
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork ?? new UnitOfWork(Context); }
            private set { _unitOfWork = value; }
        }

        [HttpPost]
        public IHttpActionResult CreateOrder(OrderModel model)
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
            drinks.AddRange(model.Drinks.Select(drink => UnitOfWork.Drinks.Get(drink.Id)));
            if (drinks.Contains(null))
            {
                return BadRequest();
            }


            //Bit of a bad way to auto increment ordernumber
            var lastOrderNumber = UnitOfWork.Orders.GetPreviousOrder().OrderNumber + 1;


            //Only way i can think to store OrderCompletedTime before it is completed is to
            //set it to max value, anything greater than todays date is not complete. 
            //Cant use nulls in datetime type
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                OrderCompletedTime = DateTime.MaxValue,
                OrderNumber = lastOrderNumber,
                OrderWord = "Humorous",
                TotalCost = drinks.Sum(drink => drink.Price),
                Paid = false,
                Completed = false,
                User = currentUser
            };
            UnitOfWork.Orders.Add(order);
            
            var stripeCharge = CreateStripeCharge(order, model.Token);
            if (stripeCharge == "succeeded")
            {
                order.Paid = true;
                order.UpdatedDateTime = DateTime.Now;
            }

            List<BasketContents> basket = new List<BasketContents>();
            basket.AddRange(drinks.Select(drink => new BasketContents
            {
                Id = Guid.NewGuid(),
                Drink = drink,
                Order = order,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            }));

            UnitOfWork.BasketContents.AddRange(basket);

            if (UnitOfWork.Save() == 0)
            {
                return InternalServerError();
            }
            
            var orderModel = _modelFactory.Create(stripeCharge, order.OrderWord, order.OrderNumber, order.Id);

            return Ok(orderModel);
        }



        private static string CreateStripeCharge(Order order, string token, string currency = "gbp")
        {
            try
            {
                var charge = new StripeChargeCreateOptions
                {
                    Amount = Convert.ToInt32(order.TotalCost * 100),
                    Description = "Order " + order.Id + " by " + order.User.Email,
                    SourceTokenOrExistingSourceId = token,
                    Currency = currency
                };

                var chargeService = new StripeChargeService();
                var stripeCharge = chargeService.Create(charge);

                //Can return 'succeeded', 'pending' or 'failed'
                return stripeCharge.Status;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return "Payment failed";
            }
        }
    }
}
