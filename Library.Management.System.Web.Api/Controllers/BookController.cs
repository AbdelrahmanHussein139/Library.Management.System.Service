using Library.Management.System.Application.DTOs.BookDTOs;
using Library.Management.System.Application.Services.BookServices;
using Library.Management.System.Web.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Management.System.Web.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ServiceFilter(typeof(LogFilter))]
[Authorize]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ILogger<BookController> _logger;

    public BookController(
        IBookService bookService,
        ILogger<BookController> logger)
    {
        _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [AllowAnonymous] // Allow all users (Admin, Member, Guest)
    [ProducesResponseType(typeof(IEnumerable<BookDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var books = await _bookService.GetAllAsync(cancellationToken);
        return Ok(books);
    }

    [HttpGet("{id:guid}", Name = "GetBookById")]
    [AllowAnonymous] // Allow all users (Admin, Member, Guest)
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await _bookService.GetByIdAsync(id, cancellationToken);
        return Ok(book);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookDTO>> CreateAsync([FromBody] CreateBookDTO createDTO, CancellationToken cancellationToken)
    {
        var book = await _bookService.CreateAsync(createDTO, cancellationToken);
        return CreatedAtRoute("GetBookById", new { id = book.Id }, book);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookDTO>> UpdateAsync(Guid id, [FromBody] UpdateBookDTO updateDTO, CancellationToken cancellationToken)
    {
        var book = await _bookService.UpdateAsync(id, updateDTO, cancellationToken);
        return Ok(book);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _bookService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
