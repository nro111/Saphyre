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
        public Task<bool> Create(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
