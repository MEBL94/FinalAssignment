using System.Collections.Generic;
using FinalAssignment.Models;

namespace FinalAssignment.Data.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        Product GetProductById(int productId);
    }
}
