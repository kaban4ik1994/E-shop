using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;

namespace WebUI.CustomAttribute
{

    public class PageAuthorizeAttribute : AuthorizeAttribute
    {
        public string UserRoles { get; set; }
        public IUserRepository Users { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authCooke = httpContext.Request.Cookies["__AUTH"];

            if (authCooke == null) return false;
            var user = Users.GetCustomerByUniqueidentifier(authCooke.Value);

         //   return UserRoles.Split(',').Any(r => r.Trim().ToLower() == user.Trim().ToLower());
            return false;
        }

    

    }
}