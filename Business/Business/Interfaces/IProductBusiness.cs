using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Interfaces
{
    public interface IProductBusiness
    {
        Task CreatAsync(ProductDTO product);
        Task UpdateAsync(ProductDTO product);
        Task DeleteAsync(int productId);
        Task<ProductDTO> GetByIdAsync(int productId);
        Task<bool> ExistsByIdAsync(int productId);
        Task<List<ProductDTO>> ListByFilterAsync(int? productId = 0, string description = null);
    }
}
