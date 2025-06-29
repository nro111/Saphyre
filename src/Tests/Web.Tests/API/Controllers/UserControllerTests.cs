using API.Controllers;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Web.Tests.API.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenNoUsersExist()
        {
            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<ActionResult<List<User>>>(result);
            Assert.NotNull(okResult.Value);
            Assert.Empty(okResult.Value);
        }

        [Fact]
        public async Task Get_ReturnsOk()
        {
            var id = Guid.NewGuid();
            var result = await _controller.Get(id);

            var okResult = Assert.IsType<OkResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsOk()
        {
            var user = new User
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

            var result = await _controller.Create(user);

            var okResult = Assert.IsType<OkResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Update_ReturnsOk()
        {
            var result = await _controller.Update(Guid.NewGuid());

            var okResult = Assert.IsType<OkResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            var result = await _controller.Delete(Guid.NewGuid());

            var okResult = Assert.IsType<OkResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}