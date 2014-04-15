using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using Domain.Abstract;

namespace WebUI.Helpers
{
    public static class AuthHelper
    {
        public static void LogInUser(HttpContextBase httpContext, string cookies)
        {
            var cookie = new HttpCookie("__AUTH")
                {
                    Value = cookies,
                    Expires = DateTime.Now.AddYears(1)
                };

            httpContext.Response.Cookies.Add(cookie);
        }


        public static void LogOffUser(HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies["__AUTH"] != null)
            {
                var cookie = new HttpCookie("__AUTH")
                    {
                        Expires = DateTime.Now.AddDays(-1),
                    };

                httpContext.Response.Cookies.Add(cookie);
            }
        }

        public static Customer GetUser(HttpContextBase httpContext, IUserRepository users)
        {
            var authCookie = httpContext.Request.Cookies["__AUTH"];
            if (authCookie != null)
            {
                var user = users.GetCustomerByUniqueidentifier(authCookie.Value);

                return user;
            }

            return null;
        }

        public static bool IsAuthenticated(HttpContextBase httpContext, IUserRepository users)
        {
            var authCookie = httpContext.Request.Cookies["__AUTH"];

            if (authCookie != null)
            {
                var user = users.GetCustomerByUniqueidentifier(authCookie.Value);

                return user != null;
            }

            return false;
        }
    }
}