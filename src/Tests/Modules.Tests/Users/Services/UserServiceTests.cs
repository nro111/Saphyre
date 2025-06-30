using Application.Interfaces;
using Contracts;
using Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
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
        public async Task GetAll_ReturnsListOfUsers()
        {
            var users = new List<UserDTO> { CreateStubUser() };
            _userRepoMock.Setup(r => r.GetAll()).ReturnsAsync(users);

            var result = await _service.GetAll();

            Assert.Equal(users, result);
        }

        [Fact]
        public async Task Get_ReturnsUser_WhenExists()
        {
            var user = CreateStubUser();
            _userRepoMock.Setup(r => r.Get(user.Id)).ReturnsAsync(user);

            var result = await _service.Get(user.Id);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task Create_ReturnsTrue_OnSuccess()
        {
            var user = CreateStubUser();
            _userRepoMock.Setup(r => r.Create(user)).ReturnsAsync(true);

            var result = await _service.Create(user);

            Assert.True(result);
        }

        [Fact]
        public async Task Update_ReturnsFalse_OnFailure()
        {
            var user = CreateStubUser();
            _userRepoMock.Setup(r => r.Update(user)).ReturnsAsync(false);

            var result = await _service.Update(user);

            Assert.False(result);
        }

        [Fact]
        public async Task Delete_ReturnsTrue_WhenSuccessful()
        {
            var id = Guid.NewGuid();
            _userRepoMock.Setup(r => r.Delete(id)).ReturnsAsync(true);

            var result = await _service.Delete(id);

            Assert.True(result);
        }

        [Fact]
        public async Task GetAll_LogsException_WhenRepositoryThrows()
        {
            _userRepoMock.Setup(r => r.GetAll()).ThrowsAsync(new Exception("DB fail"));

            var result = await Assert.ThrowsAsync<Exception>(() => _service.GetAll());

            _loggerMock.Verify(
                l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>()),
                Times.Once
            );
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
