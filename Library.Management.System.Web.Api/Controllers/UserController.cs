using Library.Management.System.Application.DTOs.UserDTOs;
using Library.Management.System.Application.Services.UserServices;
using Library.Management.System.Web.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Library.Management.System.Web.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ServiceFilter(typeof(LogFilter))]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(
        IUserService userService,
        ILogger<UserController> logger)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<UserDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync(cancellationToken);
        return Ok(users);
    }

    [HttpGet("{id:guid}", Name = "GetUserById")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken);
        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDTO>> CreateAsync([FromBody] CreateUserDTO createDTO, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateAsync(createDTO, cancellationToken);
        return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDTO>> UpdateAsync(Guid id, [FromBody] UpdateUserDTO updateDTO, CancellationToken cancellationToken)
    {
        var user = await _userService.UpdateAsync(id, updateDTO, cancellationToken);
        return Ok(user);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _userService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] SignInUserDTO signInUserDTO)
    {
        var user = await _userService.LoginAsync(signInUserDTO);
        var token = await _userService.GenerateJWTAsync(user!);
        return Ok(new
        {
            token,
            user = new { user!.Id, user!.FirstName, user!.LastName, user!.Email }
        });
    }

    
}
