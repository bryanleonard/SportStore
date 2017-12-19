using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        // Could also use IEnumerable, I think.
        // But can be less efficient as it gets ALL the stuff, not just what I need.

        void SaveProduct(Product product);

        Product DeleteProduct(int productID);
    }
}
