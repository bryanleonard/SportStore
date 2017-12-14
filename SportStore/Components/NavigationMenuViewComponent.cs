using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportStore.Models;

namespace SportStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository _repository;

        public NavigationMenuViewComponent(IProductRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewData["SelectedCategory"] = RouteData?.Values["category"];
            var result = _repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(result);
        }
    }
}
