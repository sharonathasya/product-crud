using backend_product.ViewModels;

namespace backend_product.Interfaces
{
    public interface IProductService
    {
        Task<ProductRes> AddProduct(ReqProduct request);
        Task<ProductRes> EditProduct(ReqProduct request);
        Task<ProductRes> DeleteProduct(ReqIdProduct request);
        Task<ProductRes> GetProductById(ReqIdProduct request);
        Task<ProductResList> GetProductByNamePrice(ReqIdProduct request);
        Task<ProductResList> GetProductList(ReqIdProduct request);
    }
}
