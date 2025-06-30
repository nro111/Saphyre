using API.Controllers;
using Application.Interfaces;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Web.Tests.API.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithUsers()
        {
            var users = new List<UserDTO> { CreateStubUser() };
            _userServiceMock.Setup(s => s.GetAll()).ReturnsAsync(users);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(users, okResult.Value);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithUser()
        {
            var user = CreateStubUser();
            _userServiceMock.Setup(s => s.Get(user.Id)).ReturnsAsync(user);

            var result = await _controller.Get(user.Id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsOk_WhenSuccessful()
        {
            var user = CreateStubUser();
            _userServiceMock.Setup(s => s.Create(user)).ReturnsAsync(true);

            var result = await _controller.Create(user);

            var okResult = Assert.IsType<OkResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsProblem_WhenFails()
        {
            var user = CreateStubUser();
            _userServiceMock.Setup(s => s.Create(user)).ReturnsAsync(false);

            var result = await _controller.Create(user);

            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenSuccessful()
        {
            var user = CreateStubUser();
            _userServiceMock.Setup(s => s.Update(user)).ReturnsAsync(true);

            var result = await _controller.Update(user);

            var okResult = Assert.IsType<OkResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Update_ReturnsProblem_WhenFails()
        {
            var user = CreateStubUser();
            _userServiceMock.Setup(s => s.Update(user)).ReturnsAsync(false);

            var result = await _controller.Update(user);

            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenSuccessful()
        {
            var id = Guid.NewGuid();
            _userServiceMock.Setup(s => s.Delete(id)).ReturnsAsync(true);

            var result = await _controller.Delete(id);

            var okResult = Assert.IsType<OkResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsProblem_WhenFails()
        {
            var id = Guid.NewGuid();
            _userServiceMock.Setup(s => s.Delete(id)).ReturnsAsync(false);

            var result = await _controller.Delete(id);

            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
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
