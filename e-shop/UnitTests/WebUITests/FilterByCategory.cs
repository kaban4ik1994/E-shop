using System;
using System.Linq;
using Domain;
using Domain.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Models;

namespace UnitTests.WebUITests
{
    [TestClass]
    public class FilterByCategory
    {
        [TestMethod]
        public void Can_Filter_Products()// проверка корректности фильтрации по категории
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", ProductCategory = new ProductCategory{Name = "Cat1"}},
                    new Product {ProductID = 2, Name = "P2", ProductCategory = new ProductCategory{Name = "Cat2"}},
                        new Product {ProductID = 3, Name = "P3", ProductCategory = new ProductCategory{Name = "Cat1"}},
                            new Product {ProductID = 4, Name = "P4", ProductCategory = new ProductCategory{Name = "Cat2"}},
                                new Product {ProductID = 5, Name = "P5", ProductCategory = new ProductCategory{Name = "Cat3"}}
            
            }.AsQueryable());

            var controller = new ProductController(mock.Object) { PageSize = 3 };
            var result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].ProductCategory.Name == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].ProductCategory.Name == "Cat2");
        }
    }
}

