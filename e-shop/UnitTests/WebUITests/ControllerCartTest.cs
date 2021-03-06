﻿using System;
using System.Linq;
using Domain;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Models;

namespace UnitTests.WebUITests
{
    [TestClass]
    public class ControllerCartTest
    {
        [TestMethod]
        public void Can_Add_To_Cart()// проверка корректности добавления товара в корзину
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", ProductCategory =new ProductCategory{Name = "A"}}
            }.AsQueryable());
            var cart = new Cart();
            var target = new CartController(mock.Object, null,null,null,null);

            target.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen() //корректность перехода в корзину после добавления товара
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
               new Product {ProductID = 1, Name = "P1", ProductCategory = new ProductCategory{ Name = "A"}}
            }.AsQueryable());
            var cart = new Cart();
            var target = new CartController(mock.Object, null, null, null, null);

            var result = target.AddToCart(cart, 2, "myUrl");

            //
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()//корректность передачи URL, по которому пользователь может вернуться к каталогу
        {
            var cart = new Cart();
            var target = new CartController(null,null,null,null,null);
            //
            var result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;
            //
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        [TestMethod]
        public void Change_Quantity_Product()//корректность изменения количества товара в корзине
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
               new Product {ProductID = 1, Name = "P1"}
            }.AsQueryable());

            var cart = new Cart();
            var target = new CartController(mock.Object, null, null, null, null);
            //
            target.AddToCart(cart, 1, null);//добавим
            target.ChangeQuantity(cart, 1, null,2);// сменим кол-во
            //
            Assert.AreEqual(cart.Lines.ElementAt(0).Quantity, 2);
            
        }

    }
}

