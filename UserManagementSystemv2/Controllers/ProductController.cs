using Application.Common;
using Application.DTOs.Products;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementSystemv2.Controllers
{
    [ApiController]
    [Route("api/Products/[action]")]
    public class ProductController : ControllerBase
    {
        //控制器的职责是处理用户请求，调用应用服务，并返回响应
        public readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取所有产品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _service.GetAllAsync();
            return Ok(Result<List<Product>>.DataResult(products));
        }

        /// <summary>
        /// 通过id获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductById([FromQuery] int id)
        {
            var product = await _service.GetByIdAsync(id);
            return Ok(product);
        }

        /// <summary>
        /// 通过SKU获取产品
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductBySku([FromQuery] string sku)
        {
            var product = await _service.GetBySkuAsync(sku);
            return Ok(product);
        }

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto product)
        {
            var r = await _service.AddAsync(product);
            return Ok(r != 0 ? Result<int>.DataResult(r) : Result<int>.Fail("添加产品失败!"));
            //暂时不知道怎么失败，只能先这样写着，业务失败用异常处理
        }

        /// <summary>
        /// 更新产品信息
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto product)
        {
            var r = await _service.UpdataAsync(product);
            return Ok(r ? Result.Ok() : Result.Fail("更新失败"));
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            var r = await _service.DeleteAsync(id);
            return Ok(r ? Result.Ok() : Result.Fail("删除失败"));
        }
    }
}