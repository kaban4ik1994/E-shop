﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Helpers;

namespace UnitTests.WebUITests
{
  /*  [TestClass]
    public class AdminControllerTest //не додумался как сэмулировать авторизацию
    {
        [TestMethod]
        public void Index_Contains_All_Products() //корректный возврат объектов Product
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable());
        
            var target = new AdminController(mock.Object,null);
            var result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Product() //тестирование метода edit- получения товара, при id которое есть в хранилище
        {
        
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable());
            

            var target = new AdminController(mock.Object,null);
            
            target.Edit(1);
            target.Edit(2);
            target.Edit(3);

            var p1 = target.ViewData.Model as Product;
            var p2 = target.ViewData.Model as Product;
            var p3 = target.ViewData.Model as Product;
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        //убеждаемся в том, что если id нет в хранилище, то и товара мы не получим
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable());

            var target = new AdminController(mock.Object,null);
            target.Edit(4);
            var result = (Product)target.ViewData.Model;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()//проверяем, что валидные данные передаются в хранилище
        {
            var mock = new Mock<IProductRepository>();
            var target = new AdminController(mock.Object, null);
            var product = new Product { Name = "Test" };
            var result = target.Edit(product, null);
            mock.Verify(m => m.SaveToProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes() // проверяем, что если данные не валидны, то в хранилище не передаются
        {
            var mock = new Mock<IProductRepository>();
            var target = new AdminController(mock.Object, null);
            var product = new Product { Name = "Test" };
            target.ModelState.AddModelError("error", "error");
            var result = target.Edit(product, null);
            mock.Verify(m => m.SaveToProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Products() //удаление элемента, если id есть в базе
        {
            var prod = new Product {ProductID = 3, Name = "Tests"};
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                prod,
                new Product {ProductID = 2, Name = "P2"}
            }.AsQueryable());

            var target = new AdminController(mock.Object, null);
            target.Delete(prod.ProductID);

            //утверждение того, что метод удаления из хранилище вызывается для корректного product
            mock.Verify(m=>m.DeleteProduct(prod));
        }

        [TestMethod]
        public void Cannot_Delete_Invalid_Products() //удаление элемента, если id нет в базе
        {
           
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable());

            var target = new AdminController(mock.Object, null);
            target.Delete(100);

 
            mock.Verify(m => m.DeleteProduct(It.IsAny<Product>()),Times.Never());
        }



    }*/
}
