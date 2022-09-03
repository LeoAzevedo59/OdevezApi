using Odevez.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IProductRepository
    {
        Task CreatAsync(ProductModel product);
        Task UpdateAsync(ProductModel product);
        Task DeleteAsync(int productId);
        Task<ProductModel> GetByIdAsync(int productId);
        Task<bool> ExistsByIdAsync(int productId);
        Task<List<ProductModel>> ListByFilterAsync(int? productId = 0, string description = null);
    }
}
