using Odevez.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IUserRepository
    {
        Task CreatAsync(UserModel user);
        Task UpdateAsync(UserModel user);
        Task DeleteAsync(int userId);
        Task<UserModel> GetByIdAsync(int userId);
        Task<bool> ExistsByIdAsync(int userId);
        Task<bool> ExistsByLoginAsync(int loginId);
        Task<List<UserModel>> ListByFilterAsync(int? userId = 0, string description = null);
    }
}
