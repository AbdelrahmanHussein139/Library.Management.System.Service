using Library.Management.System.Application.DTOs.LoanDTOs;
using Library.Management.System.Application.Exceptions;
using Library.Management.System.Application.Repositories;
using Library.Management.System.Domain.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Management.System.Application.Services.LoanServices;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _repository;
    private readonly IBookRepository _bookRepository;
    private readonly IBorrowerRepository _borrowerRepository;
    private readonly IMapper _mapper;

    public LoanService(ILoanRepository repository, IBookRepository bookRepository, IBorrowerRepository borrowerRepository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _borrowerRepository = borrowerRepository ?? throw new ArgumentNullException(nameof(borrowerRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<LoanDTO?> GetByIdAsync(Guid id, string? userId, string? userRole, CancellationToken cancellationToken = default)
    {
        var loan = await _repository.GetByIdAsync(id, cancellationToken);
        if (loan == null) throw new NotFoundException("Loan", id);

        // Only allow Member to view their own loan
        if (userRole == "Member" && loan.BorrowerId.ToString() != userId)
            throw new RuleException("You are not allowed to view this loan.");

        return _mapper.Map<LoanDTO>(loan);
    }

    public async Task<List<LoanDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var loans = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<LoanDTO>>(loans);
    }

    public async Task<LoanDTO> CreateAsync(CreateLoanDTO createDto, CancellationToken cancellationToken = default)
    {
        var loan = _mapper.Map<Loan>(createDto);
        var bookExists = await _bookRepository.ExistsAsync(createDto.BookId, cancellationToken);
        var borrowerExists = await _borrowerRepository.ExistsAsync(createDto.BorrowerId, cancellationToken);
        if (!bookExists)
            throw new NotFoundForeignKeyException("Book", createDto.BookId);
        if (!borrowerExists)
            throw new NotFoundForeignKeyException("Borrower", createDto.BorrowerId);
        if (await _repository.IsBookLoanedAsync(loan.BookId, cancellationToken))
            throw new RuleException("The book is already loaned out and has not been returned yet.");
        await _repository.AddAsync(loan, cancellationToken);
        return _mapper.Map<LoanDTO>(loan);
    }

    public async Task<LoanDTO?> UpdateAsync(Guid id, UpdateLoanDTO updateDto, CancellationToken cancellationToken = default)
    {
        var loan = await _repository.GetByIdAsync(id, cancellationToken);
        if (loan == null)
            throw new NotFoundException("Loan", id);
        var bookExists = await _bookRepository.ExistsAsync(updateDto.BookId, cancellationToken);
        var borrowerExists = await _borrowerRepository.ExistsAsync(updateDto.BorrowerId, cancellationToken);
        if (!bookExists)
            throw new NotFoundForeignKeyException("Book", updateDto.BookId);
        if (!borrowerExists)
            throw new NotFoundForeignKeyException("Borrower", updateDto.BorrowerId);
        if (await _repository.IsBookLoanedAsync(updateDto.BookId, cancellationToken))
            throw new RuleException("The book is already loaned out and has not been returned yet.");
        _mapper.Map(updateDto, loan);
        await _repository.UpdateAsync(loan, cancellationToken);
        return _mapper.Map<LoanDTO>(loan);
    }
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var loan = await _repository.GetByIdAsync(id, cancellationToken);
        if (loan is null)
            throw new NotFoundException("Loan", id);
        await _repository.DeleteAsync(id, cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.ExistsAsync(id, cancellationToken);
    }

    public async Task<bool> IsBookLoanedAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        return await _repository.IsBookLoanedAsync(bookId, cancellationToken);
    }
}
