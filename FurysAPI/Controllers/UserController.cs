using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using FurysAPI.App_Start;
using FurysAPI.DataAccess;
using FurysAPI.DataAccess.DataContext;
using FurysAPI.DataAccess.Entities;
using FurysAPI.Models.UserModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace FurysAPI.Controllers
{
    [Authorize]
    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;
        private FurysApiDbContext _context;

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;

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

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var defaultRole = "customer";
            //Grab the birthdate or set it to the default (Greatest day in history)
            var birthdate = model.Birthday ?? "23/04/1994";

            //ASP decides how to convert dates based on the system settings of the production environment. As the
            //server is based in US it will be MM-DD-YYYY whereas we would much prefer a more logical layout of
            //DD-MM-YYYY. To override this the cultureinfo object is inserted into the Convert.ToDateTime to
            //specify what format to use, although it may be better to actually change the system settings, this will
            //do for now
            var gbDateFormat = new CultureInfo("en-GB");

            var user = new User()
            {
                UserName = model.UserName,
                Birthday = Convert.ToDateTime(birthdate, gbDateFormat),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender
                
            };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            await UserManager.AddToRoleAsync(user.Id, defaultRole);

            return Ok("Registration successful");
        }

        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        
        [Route("ChangeName")]
        public async Task<IHttpActionResult> ChangeName(ChangeNameBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user.FirstName != model.FirstName || user.LastName != model.LastName)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                if (UnitOfWork.Save() == 0)
                {
                    return InternalServerError();
                }
            }

            return Ok();
        }
        
        #region ErrorHandling

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        #endregion

    }
}
