using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Domain;
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
                Products = _repository.Products.Where(p => category == null || p.ProductCategory.Name == category)
                .OrderBy(p => p.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category==null?_repository.Products.Count():
                    _repository.Products.Count(p => p.ProductCategory.Name==category)
                },
                CurrentCategory = category
            };

            return View(viewModel);
         
        }

    }
}
