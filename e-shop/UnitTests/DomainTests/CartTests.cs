﻿using System;
using System.Linq;
using Domain;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.DomainTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines() //тестирование добавления элементов в корзину
        {
            var p1 = new Product { ProductID = 1, Name = "P1" };
            var p2 = new Product { ProductID = 2, Name = "P2" };

            var target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            var result = target.Lines.ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[1].Product, p2);

        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines() //изменения количества товара в корзине
        {
            var p1 = new Product { ProductID = 1, Name = "P1" };
            var p2 = new Product { ProductID = 2, Name = "P2" };
            var target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            var result = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Quantity, 11);
            Assert.AreEqual(result[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()//удаление товара из корзины
        {
            var p1 = new Product { ProductID = 1, Name = "P1" };
            var p2 = new Product { ProductID = 2, Name = "P2" };
            var p3 = new Product { ProductID = 3, Name = "P3" };
            var target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            target.RemoveLine(p2);

            Assert.AreEqual(target.Lines.Count(c => c.Product == p2), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()//проверка вычисления общей стоимости элементов в корзине
        {
            var p1 = new Product { ProductID = 1, Name = "P1", StandardCost = 100M };
            var p2 = new Product { ProductID = 2, Name = "P2", StandardCost = 50M };
            var target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            var result = target.ComputeTotalValue();

            Assert.AreEqual(result, 450M);
        }

        [TestMethod]
        public void Can_Clear_Contents()//корректность сброса корзины
        {
            var p1 = new Product { ProductID = 1, Name = "P1", StandardCost = 100M };
            var p2 = new Product { ProductID = 2, Name = "P2", StandardCost = 50M };
            var target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.Clear();
            Assert.AreEqual(target.Lines.Count(), 0);
        }

        [TestMethod]
        public void Can_Change_Quantity() //корректность обновления количества товара
        {
            var p1 = new Product { ProductID = 1, Name = "P1", StandardCost= 100M };
            //   var p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            var target = new Cart();

            target.AddItem(p1, 1);
            target.ChangeQuantity(p1, 5);
            Assert.AreEqual(target.Lines.ElementAt(0).Quantity, 5);
        }
    }
}
