using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalAssignment.Data.Interfaces;
using FinalAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalAssignment.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public ProductRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> Products => _appDbContext.Products.Include(c => c.Id);
        public Product GetProductById(int productId) => _appDbContext.Products.FirstOrDefault(p => p.Id == productId);
    }
}
