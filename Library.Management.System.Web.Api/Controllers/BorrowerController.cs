using Library.Management.System.Application.DTOs.BorrowerDTOs;
using Library.Management.System.Application.Services.BorrowerServices;
using Library.Management.System.Web.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Management.System.Web.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ServiceFilter(typeof(LogFilter))]
[Authorize(Roles = "Admin")]
public class BorrowerController : ControllerBase
{
    private readonly IBorrowerService _borrowerService;
    private readonly ILogger<BorrowerController> _logger;

    public BorrowerController(
        IBorrowerService borrowerService,
        ILogger<BorrowerController> logger)
    {
        _borrowerService = borrowerService ?? throw new ArgumentNullException(nameof(borrowerService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BorrowerDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BorrowerDTO>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var borrowers = await _borrowerService.GetAllAsync(cancellationToken);
        return Ok(borrowers);
    }

    [HttpGet("{id:guid}", Name = "GetBorrowerById")]
    [ProducesResponseType(typeof(BorrowerDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BorrowerDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var borrower = await _borrowerService.GetByIdAsync(id, cancellationToken);
        return Ok(borrower);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BorrowerDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BorrowerDTO>> CreateAsync([FromBody] CreateBorrowerDTO createDTO, CancellationToken cancellationToken)
    {
        var borrower = await _borrowerService.CreateAsync(createDTO, cancellationToken);
        return CreatedAtRoute("GetBorrowerById", new { id = borrower.Id }, borrower);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(BorrowerDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BorrowerDTO>> UpdateAsync(Guid id, [FromBody] UpdateBorrowerDTO updateDTO, CancellationToken cancellationToken)
    {
        var borrower = await _borrowerService.UpdateAsync(id, updateDTO, cancellationToken);
        return Ok(borrower);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _borrowerService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
