using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository _repository;

        public NavController(IProductRepository repository)
        {
            _repository = repository;
        }

        public PartialViewResult Menu(string category=null)
        {
            ViewBag.SelectedCategory = category;
            var categories = _repository.Products.Select(x => x.ProductCategory.Name).Distinct().OrderBy(x => x);
            return PartialView(categories);
        }

       
    }
}
