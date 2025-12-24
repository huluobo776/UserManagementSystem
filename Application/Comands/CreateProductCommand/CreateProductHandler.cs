using Application.Exceptions;
using Application.Handlers;
using AutoMapper;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.Comands.CreateProductCommand
{
    public class CreateProductHandler : ICreateProductHandler
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> HandleAsync(CreateProductCommand command)
        {
            var dto = command.Dto;
            var exists = await _repo.GetBySkuAsync(dto.Sku);
            if (exists != null)
                throw new ProductException(Common.ErrorCode.ProductAlreadyExists, "SKU 已存在");

            var product = _mapper.Map<Product>(dto);
            await _repo.AddAsync(product);
            return product.Id;
        }
    }
}
