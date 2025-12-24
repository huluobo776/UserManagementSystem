using Application.DTOs.Products;
using Application.Queries.GetProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public interface IGetProductHandler
    {
        Task<ProductDto?> HandleAsync(GetProductQuery query);
    }
}
