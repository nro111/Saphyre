using Contracts;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Create(UserDTO user);
        Task<bool> Delete(Guid id);
        Task<UserDTO> Get(Guid id);
        Task<List<UserDTO>> GetAll();
        Task<bool> Update(UserDTO user);
    }
}