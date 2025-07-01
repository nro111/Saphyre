using Application.Interfaces;
using Contracts;
using Microsoft.Extensions.Logging;
using Shared;

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
        public async Task<OperationResult<bool>> Create(UserDTO user)
        {
            try
            {
                return await _userRepository.Create(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<bool>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }

        public async Task<OperationResult<bool>> Delete(Guid id)
        {
            try
            {
                return await _userRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<bool>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }

        public async Task<OperationResult<UserDTO>> Get(Guid id)
        {
            try
            {
                return await _userRepository.Get(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<UserDTO>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }

        public async Task<OperationResult<List<UserDTO>>> GetAll()
        {
            try
            {
                return await _userRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<List<UserDTO>>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }

        public async Task<OperationResult<bool>> Update(UserDTO user)
        {
            try
            {
                return await _userRepository.Update(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<bool>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }
    }
}
