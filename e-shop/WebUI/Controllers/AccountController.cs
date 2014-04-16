using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Parameters;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {

        private IUserRepository _userRepository;
        private IAddressRepository _addressRepository;
        private IAddressCustomerRepository _addressCustomerRepository;
        private ISalesOrderHeader _salesOrderHeader;

        public AccountController(IUserRepository userRepository, IAddressRepository addressRepository, IAddressCustomerRepository addressCustomerRepository, ISalesOrderHeader salesOrderHeader)
        {

            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _addressCustomerRepository = addressCustomerRepository;
            _salesOrderHeader = salesOrderHeader;
        }

        [HttpPost]
        public ActionResult Login(LogOnViewModel model)
        {
            var user = _userRepository.Users.FirstOrDefault(x => x.EmailAddress == model.UserName && x.PasswordSalt == model.Password);
            if (user != null)
            {
                Helpers.AuthHelper.LogInUser(HttpContext, user.rowguid.ToString());
            }
            return RedirectToAction("List", "Product");
        }

        public ActionResult LogOff()
        {
            Helpers.AuthHelper.LogOffUser(HttpContext);

            return RedirectToAction("List", "Product");
        }

        public ActionResult Summary()
        {
            return View();
        }

        public ActionResult ProfileUser()
        {
            var user = AuthHelper.GetUser(HttpContext, _userRepository);
            if (user != null)
                return View(user);
            return RedirectToAction("List", "Product");
        }

        public ActionResult UserAddress()
        {
            var user = AuthHelper.GetUser(HttpContext, _userRepository);
            if (user != null)
            {
                return View(_userRepository.GetAddressesByUserId(user.CustomerID));
            }
            return RedirectToAction("List", "Product");
        }


        [HttpPost]
        public ActionResult EditAddress(Address address)
        {
            var user = AuthHelper.GetUser(HttpContext, _userRepository);
            address.ModifiedDate = DateTime.Now;
            address.rowguid = Guid.NewGuid();
            if (user != null)
            {
                _addressRepository.SaveToAddress(address);
                _addressCustomerRepository.SaveToCustomerAddress(_addressCustomerRepository.BindCustomerAddress(user, address));

            }
            return RedirectToAction("ProfileUser", "Account");
        }

        [HttpPost]
        public ActionResult EditProfile(Customer customer)
        {

            var user = AuthHelper.GetUser(HttpContext, _userRepository);
            if (user != null)
            {
                customer.ModifiedDate = DateTime.Now;
                customer.PasswordHash = SecurityHelper.Hash(user.PasswordSalt);
                customer.Title = string.Empty;

                _userRepository.SaveToUser(customer);
            }
            return RedirectToAction("List", "Product");

        }

        public ActionResult ShoppingList()
        {
            var user = AuthHelper.GetUser(HttpContext, _userRepository);
            var result = _salesOrderHeader.SalesOrderHeaders.Where(x => x.CustomerID == user.CustomerID).ToList();
            return View(result);
        }

        [HttpPost]
        public ActionResult PayOff(string paymentMethod, string orderId)
        {
            IPayment<CardParameters> payment = new PaymentCard();
            if (paymentMethod == "Card")
            {

            }
            return View();
        }
    }
}
