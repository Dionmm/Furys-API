﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FurysAPI.App_Start;
using FurysAPI.DataAccess;
using FurysAPI.DataAccess.DataContext;
using FurysAPI.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace FurysAPI.Controllers
{
    [RoutePrefix("Drink")]
    [Authorize]
    public class DrinkController : ApiController
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;
        private FurysApiDbContext _context;
        private readonly IModelFactory _modelFactory;


        public DrinkController()
        {
            _modelFactory = new ModelFactory();
            PageSize = 25;
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

        public int PageSize { get; set; }

        public IHttpActionResult Get()
        {
            var drinks = UnitOfWork.Drinks.GetAll();
            var models = _modelFactory.Create(drinks);

            return Ok(models);
        }

        public IHttpActionResult Get(Guid id)
        {
            var drink = UnitOfWork.Drinks.Get(id);
            if (drink == null)
            {
                return NotFound();
            }
            var model = _modelFactory.Create(drink);
            return Ok(model);
        }
        
        public IHttpActionResult GetByBeverage(string beverageType)
        {
            var drinks = UnitOfWork.Drinks.GetByBeverageType(beverageType, PageSize, 0);
            var models = _modelFactory.Create(drinks);

            return Ok(models);
        }
    }
}
