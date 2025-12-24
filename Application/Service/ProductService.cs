using Application.Common;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.Mapping;
using AutoMapper;
using Application.Handlers;
using Application.Queries.GetProduct;
using Application.Comands.CreateProductCommand;
using Application.Comands.UpdateProduct;
using Application.DTOs.Products;

namespace Application.Service
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _repo;
        public readonly IMapper _mapper;
        public readonly ICreateProductHandler _createProductHandler;
        public readonly IGetProductHandler _getProductHandler;
        private readonly IUpdateProductHandler _updateProductHandler;

        public ProductService(IProductRepository repo, IMapper mapper, IGetProductHandler getProductHandler, ICreateProductHandler createProductHandler, IUpdateProductHandler updateProductHandler)
        {
            _repo = repo;
            _mapper = mapper;
            _getProductHandler = getProductHandler;
            _createProductHandler = createProductHandler;
            _updateProductHandler = updateProductHandler;
        }

        /*
         *   appliction层负责
         *   用例(User Case)
         *   业务规则
         *   错误语义(BusinessException)
         *   Dto <-> Entity
         *   调用Repository
         * 
         */

        /// <summary>
        /// 根据ID获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductDto?> GetByIdAsync(int id)
            => await _getProductHandler.HandleAsync(new GetProductQuery(id));

        /// <summary>
        /// 根据SKU获取产品
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public async Task<Product?> GetBySkuAsync(string sku)
            => await _repo.GetBySkuAsync(sku);

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="productInput"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(CreateProductDto productInput)
            => await _createProductHandler.HandleAsync(new CreateProductCommand(productInput));

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="productInput"></param>
        /// <returns></returns>
        public async Task<bool> UpdataAsync(UpdateProductDto productInput)
            => await _updateProductHandler.HandleAsync(new UpdateProductCommand(productInput));

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
           => await _repo.DeleteAsync(id);

        /// <summary>
        /// 获取所有产品
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> GetAllAsync()
            => await _repo.GetAllAsync();
    }
}