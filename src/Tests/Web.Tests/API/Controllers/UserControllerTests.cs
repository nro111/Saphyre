using API.Controllers;
using Application.Interfaces;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shared;

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
        public async Task GetAll_ReturnsOk_WhenSuccess()
        {
            var users = new List<UserDTO> { CreateStubUser() };
            _userServiceMock
                .Setup(s => s.GetAll())
                .ReturnsAsync(OperationResult<List<UserDTO>>.SuccessResult(users));

            var result = await _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(users, okResult.Value);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound_WhenNotFound()
        {
            _userServiceMock
                .Setup(s => s.GetAll())
                .ReturnsAsync(OperationResult<List<UserDTO>>.FailureResult("No users found", OperationStatus.NotFound));

            var result = await _controller.GetAll();
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenSuccess()
        {
            var user = CreateStubUser();
            _userServiceMock
                .Setup(s => s.Get(user.Id))
                .ReturnsAsync(OperationResult<UserDTO>.SuccessResult(user));

            var result = await _controller.Get(user.Id);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenUserMissing()
        {
            var id = Guid.NewGuid();
            _userServiceMock
                .Setup(s => s.Get(id))
                .ReturnsAsync(OperationResult<UserDTO>.FailureResult("Not found", OperationStatus.NotFound));

            var result = await _controller.Get(id);
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFound.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsOk_WhenSuccess()
        {
            var user = CreateStubUser();
            _userServiceMock
                .Setup(s => s.Create(user))
                .ReturnsAsync(OperationResult<bool>.SuccessResult(true));

            var result = await _controller.Create(user);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsServerError_WhenFails()
        {
            var user = CreateStubUser();
            _userServiceMock
                .Setup(s => s.Create(user))
                .ReturnsAsync(OperationResult<bool>.FailureResult("Something broke", OperationStatus.InternalError));

            var result = await _controller.Create(user);
            var errorResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, errorResult.StatusCode);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenSuccess()
        {
            var user = CreateStubUser();
            _userServiceMock
                .Setup(s => s.Update(user))
                .ReturnsAsync(OperationResult<bool>.SuccessResult(true));

            var result = await _controller.Update(user);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenMissing()
        {
            var user = CreateStubUser();
            _userServiceMock
                .Setup(s => s.Update(user))
                .ReturnsAsync(OperationResult<bool>.FailureResult("Missing", OperationStatus.NotFound));

            var result = await _controller.Update(user);
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFound.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenSuccess()
        {
            var id = Guid.NewGuid();
            _userServiceMock
                .Setup(s => s.Delete(id))
                .ReturnsAsync(OperationResult<bool>.SuccessResult(true));

            var result = await _controller.Delete(id);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenMissing()
        {
            var id = Guid.NewGuid();
            _userServiceMock
                .Setup(s => s.Delete(id))
                .ReturnsAsync(OperationResult<bool>.FailureResult("Missing", OperationStatus.NotFound));

            var result = await _controller.Delete(id);
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFound.StatusCode);
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
