using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repo;
        private Cart _cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            _repo = repoService;
            _cart = cartService;
        }


        public ViewResult List() => View(_repo.Orders.Where(o => !o.Shipped));


        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            Order order = _repo.Orders.FirstOrDefault(o => o.OrderID == orderId);

            if (order != null)
            {
                order.Shipped = true;
                _repo.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }


        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty. Go do some shopping!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _repo.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
        
    }
}