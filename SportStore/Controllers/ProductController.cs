using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.ViewModels;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult List(string category, int productPage = 1)
        {
            var result = new ProductsListViewModel
            {
                Products = _repository.Products
                .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    //TotalItems = _repository.Products.Count()
                    TotalItems = category == null ?
                        _repository.Products.Count()
                        : _repository.Products.Where(e => e.Category == category).Count()
                },

                CurrentCategory = category
            };
            return View(result);
        }

    }
}