using Application.Handlers;
using Domain.Interfaces;
using Application.DTOs.Products;
using System.ComponentModel.DataAnnotations;
using Application.Exceptions;
using Application.Common;

namespace Application.Comands.UpdateProduct
{
    public class UpdateProductHandler : IUpdateProductHandler
    {
        private readonly IProductRepository _repo;
        public UpdateProductHandler(IProductRepository productRepository)
        {
            _repo = productRepository;
        }

        public async Task<bool> HandleAsync(UpdateProductCommand command)
        {
            var dto = command.Dto;
            
            var existing = await _repo.GetByIdAsync(dto.Id);
            if (existing == null)
                throw new ProductException(ErrorCode.ProductNotFound, "未找到产品");

            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.Stock = dto.Stock;

            return await _repo.UpdataAsync(existing);//仓储层只做更新动作
        }


    }
}
