using Application.Interfaces;
using Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserService userService,
            ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Login(AuthenticationDTO authenticationDTO)
        {
            var result = await _userService.Login(authenticationDTO);

            return result.Status switch
            {
                OperationStatus.Ok => Ok(result.Value),
                OperationStatus.ValidationError => BadRequest(result.Error),
                OperationStatus.Unauthorized => Unauthorized(result.Error),
                OperationStatus.NotFound => NotFound(result.Error),
                OperationStatus.Conflict => Conflict(result.Error),
                OperationStatus.InternalError => StatusCode(500, result.Error),
                _ => Problem()
            };
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Register(RegistrationDTO registrationDTO)
        {
            var result = await _userService.Register(registrationDTO);

            return result.Status switch
            {
                OperationStatus.Ok => Ok(result.Value),
                OperationStatus.ValidationError => BadRequest(result.Error),
                OperationStatus.Unauthorized => Unauthorized(result.Error),
                OperationStatus.NotFound => NotFound(result.Error),
                OperationStatus.Conflict => Conflict(result.Error),
                OperationStatus.InternalError => StatusCode(500, result.Error),
                _ => Problem()
            };
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(List<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UserDTO>>> GetAll()
        {
            try
            {
                var result = await _userService.GetAll();

                return result.Status switch
                {
                    OperationStatus.Ok => Ok(result.Value),
                    OperationStatus.ValidationError => BadRequest(result.Error),
                    OperationStatus.Unauthorized => Unauthorized(result.Error),
                    OperationStatus.NotFound => NotFound(result.Error),
                    OperationStatus.Conflict => Conflict(result.Error),
                    OperationStatus.InternalError => StatusCode(500, result.Error),
                    _ => Problem()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> Get(Guid id)
        {
            try
            {
                var result = await _userService.Get(id);

                return result.Status switch
                {
                    OperationStatus.Ok => Ok(result.Value),
                    OperationStatus.ValidationError => BadRequest(result.Error),
                    OperationStatus.Unauthorized => Unauthorized(result.Error),
                    OperationStatus.NotFound => NotFound(result.Error),
                    OperationStatus.Conflict => Conflict(result.Error),
                    OperationStatus.InternalError => StatusCode(500, result.Error),
                    _ => Problem()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Create([FromBody] UserDTO user)
        {
            try
            {
                var result = await _userService.Create(user);

                return result.Status switch
                {
                    OperationStatus.Ok => Ok(result.Value),
                    OperationStatus.ValidationError => BadRequest(result.Error),
                    OperationStatus.Unauthorized => Unauthorized(result.Error),
                    OperationStatus.Conflict => Conflict(result.Error),
                    OperationStatus.InternalError => StatusCode(500, result.Error),
                    _ => Problem()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Update(UserDTO user)
        {
            try
            {
                var result = await _userService.Update(user);

                return result.Status switch
                {
                    OperationStatus.Ok => Ok(result.Value),
                    OperationStatus.ValidationError => BadRequest(result.Error),
                    OperationStatus.Unauthorized => Unauthorized(result.Error),
                    OperationStatus.Conflict => Conflict(result.Error),
                    OperationStatus.InternalError => StatusCode(500, result.Error),
                    _ => Problem()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                var result = await _userService.Delete(id);

                return result.Status switch
                {
                    OperationStatus.Ok => Ok(result.Value),
                    OperationStatus.ValidationError => BadRequest(result.Error),
                    OperationStatus.Unauthorized => Unauthorized(result.Error),
                    OperationStatus.Conflict => Conflict(result.Error),
                    OperationStatus.InternalError => StatusCode(500, result.Error),
                    _ => Problem()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem();
            }
        }
    }
}
