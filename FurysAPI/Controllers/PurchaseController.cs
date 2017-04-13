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
using Microsoft.AspNet.Identity.Owin;
using Stripe;

namespace FurysAPI.Controllers
{
    public class PurchaseController : ApiController
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;
        private FurysApiDbContext _context;
        private readonly IModelFactory _modelFactory;


        public PurchaseController()
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



        #region Helpers

        private static string CreateStripeCharge(decimal amount, string photoName, string token, string currency = "gbp")
        {
            var charge = new StripeChargeCreateOptions
            {
                Amount = Convert.ToInt32(amount * 100),
                Description = "Purchase of " + photoName,
                SourceTokenOrExistingSourceId = token,
                Currency = currency
            };

            var chargeService = new StripeChargeService();
            var stripeCharge = chargeService.Create(charge);

            //Can return 'succeeded', 'pending' or 'failed'
            return stripeCharge.Status;
        }

        private int AddPurchase()
        {
            return 0;
        }
        #endregion

    }
}
