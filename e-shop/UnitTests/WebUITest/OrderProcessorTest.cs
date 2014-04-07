using System;
using Domain;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;

namespace UnitTests.WebUITest
{
    [TestClass]
    public class OrderProcessorTest
    {
        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()//проверка того, что заказ не обрабатывается при пустой корзине
        {
            var mock = new Mock<IOrderProcessor>();
            var cart = new Cart();
            var shippingDetails = new ShippingDetails();
            var target = new CartController(null, mock.Object);
            var result = target.Checkout(cart, shippingDetails);
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),Times.Never());
           
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
            //проверятся, что заказ не обрабатывается при неверных данных о доставке
        {
            var mock = new Mock<IOrderProcessor>();
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var target = new CartController(null, mock.Object);
            target.ModelState.AddModelError("error","error");
            var result = target.Checkout(cart, new ShippingDetails());
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
            Assert.AreEqual("",result.ViewName);
            Assert.AreEqual(false,result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()//корректность обработки заказов без ошибок
        {
            var mock = new Mock<IOrderProcessor>();
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var target = new CartController(null, mock.Object);
            var result = target.Checkout(cart, new ShippingDetails());
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());
            Assert.AreEqual("Completed",result.ViewName);
            Assert.AreEqual(true,result.ViewData.ModelState.IsValid);
        }
    }
}
