using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;

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

        [HttpPost]
        public ActionResult Registration(Customer user)
        {
            user.ModifiedDate = DateTime.Now; // текущее время
            user.NameStyle = false; // под false у нас роль User (под true - Admin) 
            user.rowguid = Guid.NewGuid(); 
            user.PasswordHash = Helpers.SecurityHelper.Hash(user.PasswordSalt);
            user.PasswordSalt = user.PasswordSalt;


            _repository.SaveToUser(user);

            return RedirectToAction("List", "Product");
        } 
    }
}
