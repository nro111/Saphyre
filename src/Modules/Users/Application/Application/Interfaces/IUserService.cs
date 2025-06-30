using Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDTO>> GetAll();
        public Task<UserDTO> Get(Guid id);
        public Task<bool> Create(UserDTO user);
        public Task<bool> Update(UserDTO user);
        public Task<bool> Delete(Guid id);
    }
}
