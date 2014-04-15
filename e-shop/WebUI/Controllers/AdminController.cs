using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{

    public class AdminController : Controller
    {
        private IProductRepository _repository;
        private IProductCategoryRepository _productCategoryRepository;
        private ISalesOrderDetail _salesOrderDetail;
        private ISalesOrderHeader _salesOrderHeader;
        private IUserRepository _userRepository;

        public AdminController(IProductRepository repository, IProductCategoryRepository productCategoryRepository, ISalesOrderDetail salesOrderDetail, IUserRepository userRepository, ISalesOrderHeader salesOrderHeader)
        {
            _repository = repository;
            _productCategoryRepository = productCategoryRepository;
            _salesOrderDetail = salesOrderDetail;
            _userRepository = userRepository;
            _salesOrderHeader = salesOrderHeader;
        }

        private bool IsAdministrator()
        {
            var user = AuthHelper.GetUser(HttpContext, new EfUserRepository());
            if (user == null) return false;
            if (user.LastName == "admin" && user.PasswordSalt == "admin") return true;
            return false;
        }

        public ViewResult Index(string currentFilterName, string searchString)
        {
            var products = _repository.Products.ToList();
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilterName;
            }
            ViewBag.CurrentFilterName = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
            }


            return IsAdministrator() ? View(products) : View("Error");
        }

        public ViewResult UsersList(string currentFilterName, string searchString)
        {
            var users = _userRepository.Users.ToList();
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilterName;
            }
            ViewBag.CurrentFilterName = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper()) ||
                     s.FirstName.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            return IsAdministrator() ? View(users) : View("Error");
        }

        public ActionResult Edit(int productId)
        {
            var product = _repository.Products.FirstOrDefault(x => x.ProductID == productId);
            return IsAdministrator() ? View(product) : View("Error");

        }


        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (!IsAdministrator()) return View("Error");
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


        public ActionResult EditCategory(int productId)
        {
            if (!IsAdministrator()) return View("Error");
            ViewBag.ProductId = productId;
            return View(_productCategoryRepository.ProductCategories.ToList());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCategory(ProductCategory productCategory, int productId)
        {
            if (!IsAdministrator()) return View("Error");
            var category =
                _productCategoryRepository.ProductCategories.FirstOrDefault(x => x.Name == productCategory.Name);


            var product = _repository.Products.FirstOrDefault(x => x.ProductID == productId);
            //  product.ProductCategory = category;
            product.ProductCategoryID = category.ProductCategoryID;
            _repository.SaveToProduct(product);
            return RedirectToAction("Index");

            return View(_productCategoryRepository.ProductCategories.ToList());
        }

        public ActionResult CreateCategory(ProductCategory productCategory)
        {
            if (!IsAdministrator()) return View("Error");
            if (productCategory.Name != null)
            {
                productCategory.ModifiedDate = DateTime.Now;
                productCategory.rowguid = Guid.NewGuid();
                _productCategoryRepository.SaveToProductCategory(productCategory);
                return RedirectToAction("Index");
            }

            return View(new ProductCategory());
        }

        public ViewResult Create()
        {
            return IsAdministrator() ? View("Edit", new Product()) : View("Error");
        }

        public ActionResult StatisticsProduct(int productId)
        {
            if (!IsAdministrator()) return View("Error");
            var report = new Reports(_salesOrderDetail, _salesOrderHeader);
            TempData["Product"] = _repository.Products.FirstOrDefault(x => x.ProductID == productId).Name;
            return View(report.GetProductReport(productId));
        }

        public ActionResult StatisticsUser(int userId)
        {
            if (!IsAdministrator()) return View("Error");
            var report = new Reports(_salesOrderDetail, _salesOrderHeader);
            TempData["User"] = _userRepository.Users.FirstOrDefault(x => x.CustomerID == userId).LastName;
            return View(report.GetUserReport(userId));
        }

        public ActionResult StatisticsSales(string minDate = null, string maxDate = null)
        {
            if (!IsAdministrator()) return View("Error");

            var report = new Reports(_salesOrderDetail, _salesOrderHeader);
            ViewBag.Date = report.GetStatisticsBySales().Select(y => y.DateTime).Distinct();
            if (minDate != null && maxDate != null)
            {
                var result =
                    report.GetStatisticsBySales().Where(x => x.DateTime.CompareTo(Convert.ToDateTime(minDate)) >= 0
                                                             &&
                                                             x.DateTime.CompareTo(Convert.ToDateTime(maxDate)) <= 0);
                return View(result.ToList());
            }
            return View(report.GetStatisticsBySales());
        }


        public ActionResult ChangeSellEndDate(int productId)
        {
            var a = ModelState.IsValid;
            if (!IsAdministrator()) return View("Error");
            var product = _repository.Products.FirstOrDefault(x => x.ProductID == productId);
            if (product != null && product.SellEndDate != null)
                product.SellEndDate = null;
            else if (product != null) product.SellEndDate = DateTime.Now;
            _repository.SaveToProduct(product);
            return RedirectToAction("Index");
        }

    }
}
