using Application.DTOs.Products;
using Application.DTOs.Users;
using Application.Exceptions;
using Application.Handlers;
using Application.Queries.GetUser;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetProduct
{
    public class GetProductHandler :IGetProductHandler
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public GetProductHandler(IProductRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ProductDto?> HandleAsync(GetProductQuery query)
        {
            var product = await _repo.GetByIdAsync(query.Id);
            if (product == null)
                throw new ProductException(Common.ErrorCode.ProductNotFound, $"产品Id={query.Id}不存在");

            return _mapper.Map<ProductDto>(product);
        }
    }
}
