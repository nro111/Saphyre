using Application.Interfaces;
using Contracts;
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

        [HttpGet("all")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UserDTO>>> GetAll()
        {
            try
            {
                var result = await _userService.GetAll();
                return result.Status switch
                {
                    OperationStatus.Ok => Ok(result.Value),
                    OperationStatus.NotFound => NotFound(result.Error),
                    OperationStatus.InternalError => StatusCode(500, result.Error),
                    _ => Problem("Unexpected result status")
                };
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                return Problem("Unexpected result status");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> Get(Guid id)
        {
            try
            {              
                var result = await _userService.Get(id);
                return result.Status switch
                {
                    OperationStatus.Ok => Ok(result.Value),
                    OperationStatus.NotFound => NotFound(result.Error),
                    OperationStatus.InternalError => StatusCode(500, result.Error),
                    _ => Problem("Unexpected result status")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem();
            }
        }

        [HttpPost()]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Create([FromBody] UserDTO user)
        {
            try
            {
                var result = await _userService.Create(user);
                return result.Status switch
                {
                    OperationStatus.Ok => Ok(result.Value),
                    OperationStatus.NotFound => NotFound(result.Error),
                    OperationStatus.InternalError => StatusCode(500, result.Error),
                    _ => Problem("Unexpected result status")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> Update(UserDTO user)
        {
            try
            {
                var result = await _userService.Update(user);
                return result.Status switch
                {
                    OperationStatus.Ok => Ok(result.Value),
                    OperationStatus.NotFound => NotFound(result.Error),
                    OperationStatus.InternalError => StatusCode(500, result.Error),
                    _ => Problem("Unexpected result status")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> Delete(Guid id)
        {
            try
            {
                var result = await _userService.Delete(id);
                return result.Status switch
                {
                    OperationStatus.Ok => Ok(result.Value),
                    OperationStatus.NotFound => NotFound(result.Error),
                    OperationStatus.InternalError => StatusCode(500, result.Error),
                    _ => Problem("Unexpected result status")
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
