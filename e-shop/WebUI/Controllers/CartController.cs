using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        public CartController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }


        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null) cart.AddItem(product, 1);
            return RedirectToAction("Index", new { returnUrl });

        }

        public RedirectToRouteResult ChangeQuantity(Cart cart, int productId, string returnUrl, int quantity=0)
        {
            if (quantity < 1) quantity = 1;
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if(product!=null) cart.ChangeQuantity(product,quantity);
            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null) cart.RemoveLine(product);
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Summary(Cart cart)
        {
            return View(cart);
        }

        public ViewResult Checkout()
        {
            return View(new SalesOrderHeader());
        }

    }
}
