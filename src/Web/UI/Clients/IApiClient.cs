using Contracts;

namespace UI.Clients
{
    public interface IApiClient
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetAsync(Guid id);
        Task<bool> CreateAsync(UserDTO user);
        Task<bool> UpdateAsync(UserDTO user);
        Task<bool> DeleteAsync(Guid id);
    }
}
