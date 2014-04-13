using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {

        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }

        [HttpPost]
        public ActionResult Login(LogOnViewModel model)
        {
            var user = _userRepository.Users.FirstOrDefault(x => x.LastName == model.UserName && x.PasswordSalt == model.Password);
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
                var firstOrDefault = user.CustomerAddress.FirstOrDefault();

                return View(firstOrDefault == null ? new Address() : firstOrDefault.Address);
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
                if(user.CustomerAddress==null) user.CustomerAddress=new Collection<CustomerAddress>();
                if (user.CustomerAddress.Count == 0)
                    user.CustomerAddress.Add(new CustomerAddress()
                    {
                        Address = address,
                        AddressID = 0,
                        AddressType = "main",
                        Customer = user,
                        CustomerID = user.CustomerID,
                        ModifiedDate = DateTime.Now,
                        rowguid = Guid.NewGuid()
                    });
                else user.CustomerAddress.FirstOrDefault().Address = address;
                _userRepository.SaveToUser(user);
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

    }
}
