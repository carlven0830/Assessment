using JrAssessment.Model.Base;
using JrAssessment.Model.Database;
using JrAssessment.Model.Request;
using JrAssessment.Repository.SqLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Core.Services
{
    public interface IProductService
    {
        Task<ContentResponse<TblProduct>> AddProductAsync(ProductRequest request);
    }

    public class ProductService : IProductService
    {
        private readonly ISqLiteRepo<TblProduct> _productRepo;

        public ProductService(ISqLiteRepo<TblProduct> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<ContentResponse<TblProduct>> AddProductAsync(ProductRequest request)
        {
            var newProduct = new TblProduct
            {
                ProductName = request.ProductName,
                Price = request.Price
            };

            await _productRepo.AddAsync(newProduct);

            return ContentResponse<TblProduct>.Add(HttpStatusCode.OK, "Success add the product", newProduct);
        }
    }
}
