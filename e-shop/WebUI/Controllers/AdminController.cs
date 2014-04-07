using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _repository;

        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index()
        {
            return View(_repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(new ProductViewModel { Product = product, ProductCategory = product.ProductCategory, ProductDescription = product.ProductDescription });
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Product.ProductCategory = model.ProductCategory;
                model.Product.ProductDescription = model.ProductDescription;
                _repository.SaveProduct(model.Product);
                TempData["message"] = string.Format("{0} has been saved", model.Product.Name);
                return RedirectToAction("Index");

            }
            return View();
        }

        public ViewResult Create()
        {
            return View("Edit", new ProductViewModel{Product = new Product(),ProductCategory = new ProductCategory(), ProductDescription = new ProductDescription()});
        }
    }
}
