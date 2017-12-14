using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportStore.Data;

namespace SportStore.Models
{
    public class OrderRepository : IOrderRepository
    {
        private AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders
                            .Include(o => o.Lines)
                            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            
            if (order.OrderID == 0)
            {
                _context.Orders.Add(order);
            }

            _context.SaveChanges();
        }
    }
}
