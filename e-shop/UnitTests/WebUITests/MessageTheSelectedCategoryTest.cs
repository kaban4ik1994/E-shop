using System;
using System.Linq;
using Domain;
using Domain.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;

namespace UnitTests.WebUITests
{
    [TestClass]
    public class MessageTheSelectedCategoryTest
    {
        [TestMethod]
        public void Indicates_Selected_Category()//корректность определения выбранной категории
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", ProductSubcategory  =new ProductSubcategory{ProductCategory = new ProductCategory{Name = "A"}}},
                 new Product {ProductID = 4, Name = "P1", ProductSubcategory  =new ProductSubcategory{ProductCategory = new ProductCategory{Name = "O"}}},
            }.AsQueryable());

            var target = new NavController(mock.Object);

           
            var categoryToSelect = "A";//определение выбранной категории
            var result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            Assert.AreEqual(categoryToSelect, result);
        }
    }
    }

