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
    public class CounterProductsOfACertainCategory
    {
        [TestMethod]
        public void Generate_Category_Specific_Product_Count()//тестирование генерации корректных счетчиков товаров для различных категорий
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

            var tagret = new ProductController(mock.Object) { PageSize = 3 };

            //тестирование счетчиков товаров для различных категорий
            var res1 = ((ProductsListViewModel)tagret.List("Cat1").Model).PagingInfo.TotalItems;
            var res2 = ((ProductsListViewModel)tagret.List("Cat2").Model).PagingInfo.TotalItems;
            var res3 = ((ProductsListViewModel)tagret.List("Cat3").Model).PagingInfo.TotalItems;
            var resAll = ((ProductsListViewModel)tagret.List(null).Model).PagingInfo.TotalItems;

            //

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}

