using System.Linq;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;

namespace UnitTests.WebUITest
{
    [TestClass]
    public class MessageTheSelectedCategoryTest
    {
        [TestMethod]
        public void Indicates_Selected_Category()//корректность добавления деталей о выбранной категории
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "A"},
                new Product {ProductID = 4, Name = "P2", Category = "O"}
            }.AsQueryable());

            var target = new NavController(mock.Object);


            var categoryToSelect = "A";//определение выбранной категории
            var result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            Assert.AreEqual(categoryToSelect,result);
        }
    }
}
