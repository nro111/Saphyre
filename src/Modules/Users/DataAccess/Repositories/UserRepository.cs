using Application.Interfaces;
using Contracts;
using DataAccess.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SaphyreContext _context;
        private readonly ILogger<IUserRepository> _logger;
        public UserRepository(
            SaphyreContext context,
            ILogger<IUserRepository> logger)
        {
            _context = context;
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
