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
    [Authorize]
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
            var product = _repository.Products.FirstOrDefault(x => x.ProductID == productId);
           
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ThumbnailPhotoFileName = image.ContentType;
                    product.ThumbNailPhoto=new byte[image.ContentLength];
                    image.InputStream.Read(product.ThumbNailPhoto, 0, image.ContentLength);
                }
                _repository.SaveToProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            var prod = _repository.Products.FirstOrDefault(x => x.ProductID == productId);
            if (prod != null)
            {
                _repository.DeleteProduct(prod);
                TempData["message"] = string.Format("{0} was deleted", prod.Name);
            }
            return RedirectToAction("Index");
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

    }
}
