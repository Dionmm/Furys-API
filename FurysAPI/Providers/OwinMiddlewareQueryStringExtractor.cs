﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;

namespace FurysAPI.Providers
{
    public class OwinMiddleWareQueryStringExtractor : OwinMiddleware
    {
        public OwinMiddleWareQueryStringExtractor(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            System.Diagnostics.Debug.WriteLine("MiddleWare Begin " + context.Request.Path.Value);
            
            string bearerToken = context.Request.Query.Get("bearerToken");

            //Querystring is set as undefined if not found in localStorage
            if (bearerToken != null && bearerToken != "undefined")
            {
                var tk = Startup.OAuthOptions.AccessTokenFormat.Unprotect(bearerToken);
                var principal = new ClaimsPrincipal(tk.Identity);
                Thread.CurrentPrincipal = principal;
                context.Request.User = principal;

            }

            await Next.Invoke(context);
        }
    }
}