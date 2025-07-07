using Contracts;

namespace UI.Clients
{
    public interface IApiClient
    {
        Task<bool> LoginAsync(AuthenticationDTO authenticationDTO);
        Task<bool> RegisterAsync(RegistrationDTO registrationDTO);
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetAsync(Guid id);
        Task<bool> CreateAsync(UserDTO user);
        Task<bool> UpdateAsync(UserDTO user);
        Task<bool> DeleteAsync(Guid id);
    }
}
