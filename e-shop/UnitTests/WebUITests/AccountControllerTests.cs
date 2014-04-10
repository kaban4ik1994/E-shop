using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Infrastructure.Abstract;
using WebUI.Models;

namespace UnitTests.WebUITests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()//аутентификация при предотавлении правильных данных
        {
            var mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "admin")).Returns(true);
            var model = new LogOnViewModel { Password = "admin", UserName = "admin" };
            var target = new AccountController(mock.Object);
            var result = target.LogOn(model, "/MyURL");
            Assert.IsInstanceOfType(result,typeof(RedirectResult));
            Assert.AreEqual("/MyURL",((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()//аутентификая при предоставлении неверных данных
        {
            var mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badadmin", "badadmin")).Returns(true);
            var model = new LogOnViewModel { Password = "badadmin", UserName = "admin" };
            var target = new AccountController(mock.Object);
            var result = target.LogOn(model, "/MyURL");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }


    }
}
