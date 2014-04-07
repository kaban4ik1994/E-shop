using System.Linq;
using Domain;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Models;

namespace UnitTests.WebUITest
{
    [TestClass]
    public class ThePaginationTest
    {
        [TestMethod]
        public void Can_Paginate()// тест разбиения на страницы
        {
           var mosk= new Mock<IProductRepository>();
            mosk.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }.AsQueryable);

            var controller=new ProductController(mosk.Object) {PageSize = 3};

            var result = (ProductsListViewModel) controller.List(null,2).Model;

            var prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length==2);
            Assert.AreEqual(prodArray[0].Name,"P4");
            Assert.AreEqual(prodArray[1].Name,"P5");
        }


    }
}
