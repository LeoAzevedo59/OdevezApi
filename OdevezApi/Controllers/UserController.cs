using Odevez.Business.Interfaces;
using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class UserController : IUserBusiness
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        public async Task CreatAsync(UserDTO user)
        {
            await _userBusiness.CreatAsync(user);
        }

        public async Task DeleteAsync(int userId)
        {
            await _userBusiness.DeleteAsync(userId);
        }

        public async Task<bool> ExistsByIdAsync(int userId)
        {
            return await _userBusiness.ExistsByIdAsync(userId);
        }

        public async Task<bool> ExistsByLoginAsync(int loginId)
        {
            return await _userBusiness.ExistsByLoginAsync(loginId);
        }

        public async Task<UserDTO> GetByIdAsync(int userId)
        {
            return await _userBusiness.GetByIdAsync(userId);
        }

        public async Task<List<UserDTO>> ListByFilterAsync(int? userId = 0, string description = null)
        {
            return await _userBusiness.ListByFilterAsync(userId, description);
        }

        public async Task UpdateAsync(UserDTO user)
        {
            await _userBusiness.UpdateAsync(user);
        }
    }
}
