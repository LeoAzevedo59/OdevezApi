using Odevez.Business.Interfaces;
using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class ProductController : IProductBusiness
    {
        private readonly IProductBusiness _productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        public async Task CreatAsync(ProductDTO product)
        {
            await _productBusiness.CreatAsync(product);
        }

        public async Task DeleteAsync(int productId)
        {
            await _productBusiness.DeleteAsync(productId);
        }

        public async Task<bool> ExistsByIdAsync(int productId)
        {
            return await _productBusiness.ExistsByIdAsync(productId);
        }

        public async Task<ProductDTO> GetByIdAsync(int productId)
        {
            return await _productBusiness.GetByIdAsync(productId);
        }

        public async Task<List<ProductDTO>> ListByFilterAsync(int? productId = 0, string description = null)
        {
            return await ListByFilterAsync(productId, description);
        }

        public async Task UpdateAsync(ProductDTO product)
        {
            await _productBusiness.UpdateAsync(product);
        }
    }
}
