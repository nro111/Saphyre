using Contracts;

namespace UI.Clients
{
    public interface IApiClient
    {
        Task<bool> LoginAsync(AuthenticationDTO authentication);
        Task<bool> RegisterAsync(AuthenticationDTO authentication);
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetAsync(Guid id);
        Task<bool> CreateAsync(UserDTO user);
        Task<bool> UpdateAsync(UserDTO user);
        Task<bool> DeleteAsync(Guid id);
    }
}
