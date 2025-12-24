using Application.DTOs.Products;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto?> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetBySkuAsync(string sku);

        Task<int> AddAsync(CreateProductDto productInput);
        Task<bool> UpdataAsync(UpdateProductDto productInput);
        Task<bool> DeleteAsync(int id);
    }
}
