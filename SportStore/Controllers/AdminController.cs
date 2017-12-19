using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.ViewModels;

namespace SportStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _repo;

        public AdminController(IProductRepository repo)
        {
            _repo = repo;
        }

        // public ViewResult Index() => View(repository.Products); 
        public IActionResult Index()
        {
            return View(_repo.Products);
        }

        public ViewResult Edit(int productId)
        {
            return View(_repo.Products.FirstOrDefault(p => p.ProductID == productId));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved.";
                return RedirectToAction("Index");
            }
            else
            {
                // something went haywire
                return View(product);
            }
        }


        public ViewResult Create() => View("Edit", new Product());


        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = _repo.DeleteProduct(productId);

            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted.";
            }
            return RedirectToAction("Index");
        }
    }
}