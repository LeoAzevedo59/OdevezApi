using AutoMapper;
using Odevez.Business.Interfaces;
using Odevez.DTO;
using Odevez.Repository.Models;
using Odevez.Repository.Repositorys.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserBusiness(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task CreatAsync(UserDTO user)
        {
            await _userRepository.CreatAsync(_mapper.Map<UserModel>(user));
        }

        public async Task DeleteAsync(int userId)
        {
            await _userRepository.DeleteAsync(userId);
        }

        public async Task<bool> ExistsByIdAsync(int userId)
        {
            return await _userRepository.ExistsByIdAsync(userId);
        }

        public async Task<bool> ExistsByLoginAsync(int loginId)
        {
            return await _userRepository.ExistsByLoginAsync(loginId);
        }

        public async Task<UserDTO> GetByIdAsync(int userId)
        {
            var retorno = await _userRepository.GetByIdAsync(userId);
            return _mapper.Map<UserDTO>(retorno);
        }

        public async Task<List<UserDTO>> ListByFilterAsync(int? userId = 0, string description = null)
        {
            var retorno = await _userRepository.ListByFilterAsync(userId, description);
            return _mapper.Map<List<UserDTO>>(retorno);
        }

        public async Task UpdateAsync(UserDTO user)
        {
            await _userRepository.UpdateAsync(_mapper.Map<UserModel>(user));
        }
    }
}
