using Application.Interfaces;
using Contracts;
using Microsoft.Extensions.Logging;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<IUserService> _logger;
        public UserService(
            IUserRepository userRepository,
            ILogger<IUserService> logger
            )
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<bool> Create(UserDTO user)
        {
            try
            {
                return await _userRepository.Create(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                return await _userRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<UserDTO> Get(Guid id)
        {
            try
            {
                return await _userRepository.Get(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<UserDTO>> GetAll()
        {
            try
            {
                return await _userRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new List<UserDTO>();
            }
        }

        public async Task<bool> Update(UserDTO user)
        {
            try
            {
                return await _userRepository.Update(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
