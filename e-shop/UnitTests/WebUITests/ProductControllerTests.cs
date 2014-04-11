using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Models;

namespace UnitTests.WebUITests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void Can_Paginate()// Корректность разбиения на страницы
        {
            var mosk = new Mock<IProductRepository>();
            mosk.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }.AsQueryable);

            var controller = new ProductController(mosk.Object) { PageSize = 3 };

            var result = (ProductsListViewModel)controller.List(null, 2).Model;

            var prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Retrieve_Image_Data() //убедимся, что метод GetImage возвращает корректный тип MIME из хранилища
        {
            var prod = new Product
            {
                ProductID = 2,
                Name = "Test",
                ThumbNailPhoto = new byte[] {},
                ThumbnailPhotoFileName = "image/png"
            };

            var mosk = new Mock<IProductRepository>();
            mosk.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                prod,
                new Product {ProductID = 3, Name = "P3"}
               
            }.AsQueryable);

            var target = new ProductController(mosk.Object);
            var result = target.GetImage(2);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result,typeof(FileResult));
            Assert.AreEqual(prod.ThumbnailPhotoFileName,result.ContentType);

        }

        [TestMethod]
        public void Cannor_Retrieve_Image_Data_For_Invalid_ID()// убедимся в том, что никакие данные не возрвращаются, если идентификатор не существует
        {
            var mosk = new Mock<IProductRepository>();
            mosk.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 3, Name = "P3"}
               
            }.AsQueryable);
            var target = new ProductController(mosk.Object);
            var result = target.GetImage(2);
            Assert.IsNull(result);
            

        }


    }
}
