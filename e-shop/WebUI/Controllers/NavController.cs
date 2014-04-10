using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository _repository;

        public NavController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            
            var categories =
                _repository.Products.
                Where(x=>x.ProductCategory!=null)
                .Select(x => x.ProductCategory.Name).Distinct().OrderBy(x => x);
         
            return PartialView(categories);
        }
       
    }
}
