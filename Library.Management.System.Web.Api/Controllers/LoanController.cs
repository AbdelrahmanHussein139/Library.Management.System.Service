using Library.Management.System.Application.DTOs.LoanDTOs;
using Library.Management.System.Application.Services.LoanServices;
using Library.Management.System.Web.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Management.System.Web.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ServiceFilter(typeof(LogFilter))]
[Authorize]
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService;
    private readonly ILogger<LoanController> _logger;

    public LoanController(
        ILoanService loanService,
        ILogger<LoanController> logger)
    {
        _loanService = loanService ?? throw new ArgumentNullException(nameof(loanService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<LoanDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LoanDTO>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var loans = await _loanService.GetAllAsync(cancellationToken);
        return Ok(loans);
    }

    [HttpGet("{id:guid}", Name = "GetLoanById")]
    [Authorize(Roles = "Admin,Member")]
    [ProducesResponseType(typeof(LoanDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LoanDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userRole = User.IsInRole("Admin") ? "Admin" : (User.IsInRole("Member") ? "Member" : null);
        var loan = await _loanService.GetByIdAsync(id, userId, userRole, cancellationToken);
        return Ok(loan);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(LoanDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoanDTO>> CreateAsync([FromBody] CreateLoanDTO createDTO, CancellationToken cancellationToken)
    {
        var loan = await _loanService.CreateAsync(createDTO, cancellationToken);
        return CreatedAtRoute("GetLoanById", new { id = loan.Id }, loan);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(LoanDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoanDTO>> UpdateAsync(Guid id, [FromBody] UpdateLoanDTO updateDTO, CancellationToken cancellationToken)
    {
        var loan = await _loanService.UpdateAsync(id, updateDTO, cancellationToken);
        return Ok(loan);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _loanService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
