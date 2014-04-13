using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.Concrete;
using WebUI.Helpers;
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

        private bool IsAdministrator()
        {
            var user = AuthHelper.GetUser(HttpContext, new EfUserRepository());
            if (user == null) return false;
            if (user.LastName == "admin" && user.PasswordSalt == "admin") return true;
            return false;
        }

        public ViewResult Index()
        {
           
            return IsAdministrator() ? View(_repository.Products) : View("Error");
        }

        public ActionResult Edit(int productId)
        {
            var product = _repository.Products.FirstOrDefault(x => x.ProductID == productId);
            return IsAdministrator() ? View(product) : View("Error");
         
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ThumbnailPhotoFileName = image.ContentType;
                    product.ThumbNailPhoto = new byte[image.ContentLength];
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
           return IsAdministrator() ? View("Edit",new Product()) : View("Error");
        }



    }
}
