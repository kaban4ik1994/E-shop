using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebUI.HtmlHelpers;
using WebUI.Models;

namespace UnitTests.WebUITest
{
    [TestClass]
    public class CreateLinksToPages
    {
        [TestMethod]
        public void Can_Generate_Page_Links()//тестирование метода PageLinks
        {
            HtmlHelper myHelper = null;
            var pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            var result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>)
<a class=""selected"" href=""Page2"">2</a><a href=""Page3"">3</a>");


        }
    }
}
