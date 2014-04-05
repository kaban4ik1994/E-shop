using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Domain.Abstract;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 4;
        private IProductRepository _repository;

        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public ViewResult List(int page=1)
        {
            return View(_repository.Products.Skip((page-1)*PageSize).Take(PageSize));
        }

    }
}
