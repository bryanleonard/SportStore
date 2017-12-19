using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class FakeProductRepository /* : IProductRepository */ 
    {
        public IQueryable<Product> Products => new List<Product> {
            new Product { Name = "Football", Price = 25 },
            new Product { Name = "Surf board", Price = 179 },
            new Product { Name = "Running shoes", Price = 95 },
            new Product { Name = "Racing Drone", Price = 1195 }
        }.AsQueryable<Product>();

        //AsQueryable converts the fixed collection to an IQueryable<Product>
        //Subverts having to deal with real queries.
    }
}
