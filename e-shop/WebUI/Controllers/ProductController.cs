using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 30;
        private IProductRepository _repository;

        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public ViewResult List(string category, int page=1)
        {
          
            var viewModel = new ProductsListViewModel
            {
                Products = _repository.Products
                .Where(p=>category==null||p.ProductSubcategory.ProductCategory.Name==category)
                .OrderBy(p => p.ProductID)
                    .Skip((page - 1)*PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category==null? _repository.Products.Count(): _repository.Products.Count(p=>p.ProductSubcategory.ProductCategory.Name==category)
                },
                CurrentCategory = category

            };
            return View(viewModel);
        }

    }
}