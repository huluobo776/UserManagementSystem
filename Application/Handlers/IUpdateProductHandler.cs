using Application.Comands.UpdateProduct;
using Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public interface IUpdateProductHandler
    {
        Task<bool> HandleAsync(UpdateProductCommand dto);
    }
}
