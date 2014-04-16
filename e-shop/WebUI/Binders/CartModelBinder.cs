using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Domain.Entities;


namespace WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string _sessionKey = "Cart";


        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cart = controllerContext.HttpContext.Session[_sessionKey];
            if (cart != null) return cart;
            cart = new Cart();
            controllerContext.HttpContext.Session[_sessionKey] = cart;
            return cart;
        }
    }
}