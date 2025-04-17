using backend_product.Helpers;
using backend_product.Interfaces;
using backend_product.Models;
using backend_product.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace backend_product.Impl
{
    public class ProductService(dbContext context, ITokenManager tokenManager) : IProductService
    {
        private readonly ITokenManager _tokenManager = tokenManager;
        private readonly dbContext _dbContext = context;

        #region Add Product
        public async Task<ProductRes> AddProduct(ReqProduct request)
        {
            DateTime aDate = DateTime.Now;
            ProductRes productRes = new();

            try
            {
                var currentUser = tokenManager.GetPrincipal();
                var checkdata = context.Product.Where(x => x.Name == request.Name).FirstOrDefault();
                if (checkdata == null)
                {
                    Product insertProduct = new Product
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Price = request.Price,
                        CreatedAt = aDate
                    };
                    context.Product.Add(insertProduct);
                    context.SaveChanges();


                    productRes.STATUS = Constants.ResponseConstant.Success;
                    productRes.MESSAGE = Constants.ResponseConstant.SubmitSuccess;
                }
                else
                {
                    productRes.STATUS = Constants.ResponseConstant.Failed;
                    productRes.MESSAGE = Constants.ResponseConstant.ProductNameExist;
                }

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                productRes.STATUS = Constants.ResponseConstant.Failed;
                productRes.MESSAGE = errmsg;
            }

            return productRes;
        }
        #endregion

        #region Edit Product
        public async Task<ProductRes> EditProduct(ReqProduct request)
        {
            DateTime aDate = DateTime.Now;
            ProductRes productRes = new();

            try
            {
                var currentUser = tokenManager.GetPrincipal();
                var dataProduct = context.Product.Where(x => x.Id == request.Id).FirstOrDefault();
                if (dataProduct != null)
                {
                    dataProduct.Name = request.Name;
                    dataProduct.Description = request.Description;
                    dataProduct.Price = request.Price;
                    context.Product.Update(dataProduct);
                    context.SaveChanges();

                    productRes.STATUS = Constants.ResponseConstant.Success;
                    productRes.MESSAGE = Constants.ResponseConstant.SubmitSuccess;
                }
                else
                {
                    productRes.STATUS = Constants.ResponseConstant.Failed;
                    productRes.MESSAGE = Constants.ResponseConstant.SubmitFailed;
                }

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                productRes.STATUS = Constants.ResponseConstant.Failed;
                productRes.MESSAGE = errmsg;
            }

            return productRes;
        }
        #endregion

        #region Delete
        public async Task<ProductRes> DeleteProduct(ReqIdProduct request)
        {
            ProductRes productRes = new();
            DataProduct dataProduct = new();
            int productId = 0;
            if (request.Id != null)
            {
                productId = int.Parse(request.Id);
            }

            try
            {
                var getData = context.Product.Where(x => x.Id == productId).FirstOrDefault();
                if (getData != null)
                {
                    context.Product.Remove(getData);
                    context.SaveChanges();

                    productRes.STATUS = Constants.ResponseConstant.Success;
                    productRes.MESSAGE = Constants.ResponseConstant.DeleteSuccess;
                    productRes.RESULT = dataProduct;
                }
                else
                {
                    productRes.STATUS = Constants.ResponseConstant.NotFound;
                    productRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                productRes.STATUS = Constants.ResponseConstant.Failed;
                productRes.MESSAGE = errmsg;
            }
            return productRes;
        }
        #endregion

        #region Get Product By Id
        public async Task<ProductRes> GetProductById(ReqIdProduct request)
        {
            ProductRes productRes = new();
            DataProduct dataProduct = new();
            int productId = 0;
            if (request.Id != null) 
            { 
                productId = int.Parse(request.Id);
            }

            try
            {
                var getData = context.Product.Where(x => x.Id == productId).FirstOrDefault();
                if (getData != null)
                {
                    dataProduct = new()
                    {
                        Id = getData.Id,
                        Name = getData.Name,
                        Description = getData.Description,
                        Price = getData.Price,
                        CreatedAt = getData.CreatedAt
                    };
                    productRes.STATUS= Constants.ResponseConstant.Success;
                    productRes.MESSAGE= Constants.ResponseConstant.DataFound;
                    productRes.RESULT = dataProduct;
                }
                else
                {
                    productRes.STATUS = Constants.ResponseConstant.NotFound;
                    productRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex) 
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                productRes.STATUS = Constants.ResponseConstant.Failed;
                productRes.MESSAGE = errmsg;
            }
            return productRes;
        }
        #endregion

        
        #region Get Product By Name or Price
        public async Task<ProductResList> GetProductByNamePrice(ReqIdProduct request)
        {
            ProductResList productRes = new();
            List<DataProduct> listProduct = [];
            decimal priceDec = 0;
            if (request.Price != null)
            {
                priceDec = decimal.Parse(request.Price.ToString());
            }

            try
            {
                var getData = context.Product.Where(x => x.Name == request.Name || x.Price == priceDec).ToList();
                if (getData != null && getData.Any())
                {
                    foreach(var result in getData)
                    {
                        DataProduct product = new()
                        {
                            Id = result.Id,
                            Name = result.Name,
                            Description = result.Description,
                            Price = result.Price,
                            CreatedAt = result.CreatedAt
                        };
                        listProduct.Add(product);
                    };

                    productRes.STATUS = Constants.ResponseConstant.Success;
                    productRes.MESSAGE = Constants.ResponseConstant.DataFound;
                    productRes.RESULT = listProduct;
                }
                else
                {
                    productRes.STATUS = Constants.ResponseConstant.NotFound;
                    productRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                productRes.STATUS = Constants.ResponseConstant.Failed;
                productRes.MESSAGE = errmsg;
            }
            return productRes;
        }
        #endregion

        #region Get Product List
        public async Task<ProductResList> GetProductList(ReqIdProduct request)
        {
            ProductResList productRes = new();
            List<DataProduct> listProduct = [];

            try
            {
                var getData = context.Product.ToList();
                if (getData != null && getData.Any())
                {
                    foreach (var result in getData)
                    {
                        DataProduct product = new()
                        {
                            Id = result.Id,
                            Name = result.Name,
                            Description = result.Description,
                            Price = result.Price,
                            CreatedAt = result.CreatedAt
                        };
                        listProduct.Add(product);
                    };

                    productRes.STATUS = Constants.ResponseConstant.Success;
                    productRes.MESSAGE = Constants.ResponseConstant.DataFound;
                    productRes.RESULT = listProduct;
                }
                else
                {
                    productRes.STATUS = Constants.ResponseConstant.NotFound;
                    productRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                productRes.STATUS = Constants.ResponseConstant.Failed;
                productRes.MESSAGE = errmsg;
            }
            return productRes;
        }
        #endregion

    }
}
