using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class EfCoreProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public EfCoreProductRepository(AppDbContext context) => _context = context;
        
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _context.Products.FindAsync(id);
            if (p == null) return false;
            _context.Products.Remove(p);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAllAsync()
            => await _context.Products.AsNoTracking().ToListAsync();

        public async Task<Product?> GetByIdAsync(int id)
            => await _context.Products.FindAsync(id);

        public async Task<Product?> GetBySkuAsync(string sku)
            => await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Sku == sku);


        public async Task<bool> UpdataAsync(Product product)
        {
            var existing = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if(existing == null) return false;
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Stock = product.Stock;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
