using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetBySkuAsync(string sku);

        Task AddAsync(Product product);
        Task<bool> UpdataAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}
