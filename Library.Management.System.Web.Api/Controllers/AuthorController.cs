using Library.Management.System.Application.DTOs.AuthorDTOs;
using Library.Management.System.Application.Services.AuthorServices;
using Library.Management.System.Web.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Library.Management.System.Web.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ServiceFilter(typeof(LogFilter))]
[Authorize(Roles ="Admin")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly ILogger<AuthorController> _logger;

    public AuthorController(
        IAuthorService authorService,
        ILogger<AuthorController> logger)
    {
        _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AuthorDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var authors = await _authorService.GetAllAsync(cancellationToken);
        return Ok(authors);
    }

    [HttpGet("{id:guid}", Name = "GetAuthorById")]
    [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var author = await _authorService.GetByIdAsync(id, cancellationToken);
        return Ok(author);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorDTO>> CreateAsync([FromBody] CreateAuthorDTO createDTO, CancellationToken cancellationToken)
    {
        var author = await _authorService.CreateAsync(createDTO, cancellationToken);
        return CreatedAtRoute("GetAuthorById", new { id = author.Id }, author);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorDTO>> UpdateAsync(Guid id, [FromBody] UpdateAuthorDTO updateDTO, CancellationToken cancellationToken)
    {
        var author = await _authorService.UpdateAsync(id, updateDTO, cancellationToken);
        return Ok(author);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _authorService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
