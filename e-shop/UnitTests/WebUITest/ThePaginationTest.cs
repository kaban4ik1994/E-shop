using System.Linq;
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
                new Product {ProductID = 1, Name = "p1"},
                new Product {ProductID = 2, Name = "p2"},
                new Product {ProductID = 3, Name = "p3"},
                new Product {ProductID = 4, Name = "p4"},
                new Product {ProductID = 5, Name = "p5"}
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
