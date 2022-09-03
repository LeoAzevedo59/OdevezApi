using Odevez.DTO;
using Odevez.Utils.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task CreatAsync(UserDTO user);
        Task UpdateAsync(UserDTO user);
        Task DeleteAsync(int userId);
        Task<UserDTO> GetByIdAsync(int userId);
        Task<bool> ExistsByIdAsync(int userId);
        Task<bool> ExistsByLoginAsync(int loginId);
        Task<List<UserDTO>> ListByFilterAsync(int? userId = 0, string description = null);
    }
}
