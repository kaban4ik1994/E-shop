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
    public class DataPaginationTest
    {
        [TestMethod]
        public void Can_Send_Pagination_View_Model()   // необходимо удостовериться, что контроллер отправляет представлению правильные данные разбиения страниц
        {
            //создание имитированного хранилища
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "p1"},
                new Product {ProductID = 2, Name = "p2"},
                new Product {ProductID = 3, Name = "p3"},
                new Product {ProductID = 4, Name = "p4"},
                new Product {ProductID = 5, Name = "p5"}
            }.AsQueryable());

            //создание контроллера и установка размера
            var controller = new ProductController(mock.Object) { PageSize = 3 };
            //действие
            var result = (ProductsListViewModel)controller.List(null, null, null, null, null, null, null, null, 1).Model;
            // утверждение
            var pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 1);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);

        }
    }
}
