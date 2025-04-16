using backend_product.Impl;
using backend_product.Interfaces;
using backend_product.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService productService = productService;

        [Authorize]
        [HttpPost("AddProduct")]
        public async Task<ProductRes> AddProduct([FromBody] ReqProduct request)
        {
            return await productService.AddProduct(request);
        }

        [Authorize]
        [HttpPost("EditProduct")]
        public async Task<ProductRes> EditProduct([FromBody] ReqProduct request)
        {
            return await productService.EditProduct(request);
        }

        [Authorize]
        [HttpPost("DeleteProduct")]
        public async Task<ProductRes> DeleteProduct([FromBody] ReqIdProduct request)
        {
            return await productService.DeleteProduct(request);
        }

        [Authorize]
        [HttpPost("GetProductById")]
        public async Task<ProductRes> GetProductById([FromBody] ReqIdProduct request)
        {
            return await productService.GetProductById(request);
        }

        [Authorize]
        [HttpPost("GetProductByNamePrice")]
        public async Task<ProductResList> GetProductByNamePrice([FromBody] ReqIdProduct request)
        {
            return await productService.GetProductByNamePrice(request);
        }

        [Authorize]
        [HttpPost("GetProductList")]
        public async Task<ProductResList> GetProductList([FromBody] ReqIdProduct request)
        {
            return await productService.GetProductList(request);
        }

    }
}
