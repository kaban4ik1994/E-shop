using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Ninject.Infrastructure.Language;
using PagedList;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 20;
        private IProductRepository _repository;

        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }


        public ViewResult List(string sortOrder, string currentFilterName, string currentFilterMinCost, string currentFilterMaxCost, string searchString, string minCost, string maxCost, string category, int page = 1)
        {

            var products = _repository.Products.Where(x => x.SellEndDate == null).ToList();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.CostSortParam = sortOrder == "Cost" ? "Cost desc" : "Cost";
            if (Request != null && Request.HttpMethod == "GET")
            {
                searchString = currentFilterName;
                minCost = currentFilterMinCost;
                maxCost = currentFilterMaxCost;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilterName = searchString;
            ViewBag.CurrentFilterMinCost = minCost;
            ViewBag.CurrentFilterMaxCost = maxCost;

            //

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            decimal dminCost;
            decimal dmaxCost;

            if (!String.IsNullOrEmpty(minCost) && !String.IsNullOrEmpty(maxCost) && decimal.TryParse(minCost, out dminCost) && decimal.TryParse(maxCost, out dmaxCost))
            {

                products =
                    products.Where(
                        s => s.StandardCost >= dminCost && s.StandardCost <= dmaxCost)
                        .ToList();
            }

            switch (sortOrder)
            {
                case "Name desc":
                    products = products.OrderByDescending(s => s.Name).ToList();
                    break;
                case "Cost":
                    products = products.OrderBy(s => s.StandardCost).ToList();
                    break;
                case "Cost desc":
                    products = products.OrderByDescending(s => s.StandardCost).ToList();
                    break;
                case "Name":
                    products = products.OrderBy(s => s.Name).ToList();
                    break;
            }


            var viewModel = new ProductsListViewModel
            {
                Products = products
                .Where(p => category == null || p.ProductCategory.Name == category)
                    //   .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? products.Count() : products.Count(p => p.ProductCategory.Name == category)
                },
                CurrentCategory = category,
            };

            return View(viewModel);
        }

        public FileContentResult GetImage(int productId)
        {
            var prod = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return prod != null ? File(prod.ThumbNailPhoto, prod.ThumbnailPhotoFileName) : null;
        }



    }
}