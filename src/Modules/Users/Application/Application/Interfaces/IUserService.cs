using Contracts;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserService
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
