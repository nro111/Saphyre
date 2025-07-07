using Contracts;
using Shared;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<OperationResult<bool>> Login(AuthenticationDTO authenticationDTO);
        Task<OperationResult<bool>> Register(RegistrationDTO registrationDTO);
        Task<OperationResult<bool>> Create(UserDTO user);
        Task<OperationResult<bool>> Delete(Guid id);
        Task<OperationResult<UserDTO>> Get(Guid id);
        Task<OperationResult<List<UserDTO>>> GetAll();
        Task<OperationResult<bool>> Update(UserDTO user);
    }
}