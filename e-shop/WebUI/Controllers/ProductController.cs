using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Domain.Abstract;
using Domain.Concrete;
using WebUI.Models;

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

        public ViewResult List(string category, int page=1)
        {
           
            var viewModel = new ProductsListViewModel
            {
                Products = _repository.Products.Where(p=>category==null||p.Category==category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1)*PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Products.Count()
                },
                CurrentCategory = category
            };

            return View(viewModel);
         
        }

    }
}
