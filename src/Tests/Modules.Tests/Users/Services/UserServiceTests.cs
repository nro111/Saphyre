using Application.Interfaces;
using Contracts;
using Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<ILogger<IUserService>> _loggerMock;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<IUserService>>();
            _service = new UserService(_userRepoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsSuccess_WhenRepositorySucceeds()
        {
            var users = new List<UserDTO> { CreateStubUser() };
            _userRepoMock
                .Setup(r => r.GetAll())
                .ReturnsAsync(OperationResult<List<UserDTO>>.SuccessResult(users));

            var result = await _service.GetAll();

            Assert.True(result.Success);
            Assert.Equal(users, result.Value);
        }

        [Fact]
        public async Task Get_ReturnsUser_WhenExists()
        {
            var user = CreateStubUser();
            _userRepoMock
                .Setup(r => r.Get(user.Id))
                .ReturnsAsync(OperationResult<UserDTO>.SuccessResult(user));

            var result = await _service.Get(user.Id);

            Assert.True(result.Success);
            Assert.Equal(user, result.Value);
        }

        [Fact]
        public async Task Create_ReturnsSuccess_WhenRepositorySucceeds()
        {
            var user = CreateStubUser();
            _userRepoMock
                .Setup(r => r.Create(user))
                .ReturnsAsync(OperationResult<bool>.SuccessResult(true));

            var result = await _service.Create(user);

            Assert.True(result.Success);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task Update_ReturnsFailure_WhenRepositoryFails()
        {
            var user = CreateStubUser();
            _userRepoMock
                .Setup(r => r.Update(user))
                .ReturnsAsync(OperationResult<bool>.FailureResult("Update failed", OperationStatus.Conflict));

            var result = await _service.Update(user);

            Assert.False(result.Success);
            Assert.Equal(OperationStatus.Conflict, result.Status);
        }

        [Fact]
        public async Task Delete_ReturnsSuccess_WhenRepositorySucceeds()
        {
            var id = Guid.NewGuid();
            _userRepoMock
                .Setup(r => r.Delete(id))
                .ReturnsAsync(OperationResult<bool>.SuccessResult(true));

            var result = await _service.Delete(id);

            Assert.True(result.Success);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task GetAll_ReturnsFailureResult_WhenRepositoryThrows()
        {
            _userRepoMock
                .Setup(r => r.GetAll())
                .ThrowsAsync(new Exception("DB failure"));

            var result = await _service.GetAll();

            Assert.False(result.Success);
        }

        private static UserDTO CreateStubUser()
        {
            return new UserDTO
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                Phone = "5551234567",
                AddressLine1 = "123 Main St",
                AddressLine2 = "Apt 4B",
                City = "Springfield",
                State = "IL",
                PostalCode = "62704",
                DateOfBirth = new DateTime(1990, 5, 21)
            };
        }
    }
}
