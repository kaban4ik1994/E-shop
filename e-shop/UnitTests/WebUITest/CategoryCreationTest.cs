using System.Collections.Generic;
using System.Linq;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;

namespace UnitTests.WebUITest
{
    [TestClass]
    public class CategoryCreationTest
    {
        [TestMethod]
        public void Can_Create_Categories()//корректность построения списка категорий
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "A"},
                new Product {ProductID = 2, Name = "P2", Category = "A"},
                new Product {ProductID = 3, Name = "P3", Category = "P"},
                new Product {ProductID = 4, Name = "P4", Category = "O"}
            }.AsQueryable());

            var tagret = new NavController(mock.Object);

            var result = ((IEnumerable<string>)tagret.Menu().Model).ToArray();

            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "A");
            Assert.AreEqual(result[1], "O");
            Assert.AreEqual(result[2], "P");
        }
    }
}
