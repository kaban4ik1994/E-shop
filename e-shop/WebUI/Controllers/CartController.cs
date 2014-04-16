using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        private IOrderProcessor _orderProcessor;
        private IAddressCustomerRepository _addressCustomerRepository;
        private IUserRepository _userRepository;
        private IAddressRepository _addressRepository;

        public CartController(IProductRepository repository, IOrderProcessor orderProcessor, IAddressCustomerRepository addressCustomerRepository, IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _repository = repository;
            _orderProcessor = orderProcessor;
            _addressCustomerRepository = addressCustomerRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
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
            if (product != null) cart.ChangeQuantity(product, quantity);
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

        public ViewResult Checkout(Cart cart)
        {
            var user = AuthHelper.GetUser(HttpContext, new EfUserRepository());
            var result = new ShippingDetailsModel
            {
                Cart = cart,
                Address = _userRepository.GetAddressesByUserId(user.CustomerID)
            };

            return View(result);
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, Address address, bool mainAddress, string shipMethod)
        {
            if(!cart.Lines.Any()) ModelState.AddModelError("","Sorry, your cart is empty!");
            var user = AuthHelper.GetUser(HttpContext, new EfUserRepository());
            if (mainAddress) //если основной, то перезаписываем
            {
                if (address.AddressID == 0) //на случай, если пользователь не заполнил свой адрес в профиле
                {
                    address.ModifiedDate = DateTime.Now;
                    address.rowguid = Guid.NewGuid();
                    _addressRepository.SaveToAddress(address);
                    _addressCustomerRepository.SaveToCustomerAddress(_addressCustomerRepository.BindCustomerAddress(
                       user, address));
                }
                else
                {
                    _addressRepository.SaveToAddress(address);
                }
            }
            else // иначе добавляем новый
            {
                address.AddressID = 0;
                address.ModifiedDate = DateTime.Now;
                address.rowguid = Guid.NewGuid();
                _addressRepository.SaveToAddress(address);
            }
            if (ModelState.IsValid)
            {
               _orderProcessor.Processor(cart, user,address, shipMethod);
                cart.Clear();
                return View("Completed");
            }
            return View("Checkout");
        }


    }
}
