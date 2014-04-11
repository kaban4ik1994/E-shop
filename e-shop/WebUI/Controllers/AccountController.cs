using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using WebUI.Infrastructure.Abstract;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider _authProvider;
        private IUserRepository _userRepository;

        public AccountController(IAuthProvider authProvider, IUserRepository userRepository)
        {
            _authProvider = authProvider;
            _userRepository = userRepository;
        }

        [HttpPost]
        public ActionResult Login(LogOnViewModel model)
        {
           var user = _userRepository.Users.FirstOrDefault(x=>x.LastName==model.UserName&&x.PasswordSalt==model.Password);
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
    }
}
