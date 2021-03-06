﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.Concrete;
using WebUI.Helpers;

namespace WebUI.Controllers
{
    public class RegistrationController : Controller
    {

        private IUserRepository _repository;

        public RegistrationController(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        public ActionResult Registration()
        {
            return View();
        }

        public JsonResult CheckForExist(string emailAddress)
        {
            var user = AuthHelper.GetUser(HttpContext, new EfUserRepository());
            bool result;
            if (user != null)
            {
                result =
                   _repository.Users.FirstOrDefault(
                       x => x.EmailAddress == emailAddress && x.CustomerID != user.CustomerID) == null;
            }
            else
            {
                result =
                  _repository.Users.FirstOrDefault(
                      x => x.EmailAddress == emailAddress) == null;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Registration(Customer user)
        {
            user.ModifiedDate = DateTime.Now; // текущее время
            user.NameStyle = false;
            user.rowguid = Guid.NewGuid();
            user.PasswordHash = Helpers.SecurityHelper.Hash(user.PasswordSalt);
            user.PasswordSalt = user.PasswordSalt;


            _repository.SaveToUser(user);

            return RedirectToAction("List", "Product");
        }
    }
}
