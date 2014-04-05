using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;

namespace WebUI.Binders
{
    public class CartModelBinder:IModelBinder
    {
        private const string SessionKey = "Cart";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //достаем cart из сеанса 
            var cart = (Cart) controllerContext.HttpContext.Session[SessionKey];
            //если нет в сеансе, то создаем новый
            if (cart != null) return cart;
            cart=new Cart();
            controllerContext.HttpContext.Session[SessionKey] = cart;
            return cart;
        }
    }
}