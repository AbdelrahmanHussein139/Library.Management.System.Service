using Library.Management.System.Application.DTOs.BorrowerDTOs;
using Library.Management.System.Application.Repositories;
using Library.Management.System.Application.Exceptions;
using MapsterMapper;
using Library.Management.System.Domain.Entities;

namespace Library.Management.System.Application.Services.BorrowerServices;

public class BorrowerService : IBorrowerService
{
    private readonly IBorrowerRepository _repository;
    private readonly IMapper _mapper;


    public BorrowerService(IBorrowerRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }

    public async Task<BorrowerDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var borrower = await _repository.GetByIdAsync(id, cancellationToken);

        return borrower == null ? null : _mapper.Map<BorrowerDTO>(borrower);
    }

    public async Task<List<BorrowerDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var borrowers = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<BorrowerDTO>>(borrowers);
    }

    public async Task<BorrowerDTO> CreateAsync(CreateBorrowerDTO createDto, CancellationToken cancellationToken = default)
    {
        var borrower = _mapper.Map<Borrower>(createDto);
        await _repository.AddAsync(borrower, cancellationToken);
        return _mapper.Map<BorrowerDTO>(borrower);
    }

    public async Task<BorrowerDTO?> UpdateAsync(Guid id, UpdateBorrowerDTO updateDto, CancellationToken cancellationToken = default)
    {
        var borrower = await _repository.GetByIdAsync(id, cancellationToken);
        if (borrower == null)
            throw new NotFoundException("Borrower", id);
        _mapper.Map(updateDto, borrower);
        await _repository.UpdateAsync(borrower, cancellationToken);
        return _mapper.Map<BorrowerDTO>(borrower);
    }
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var borrower = await _repository.GetByIdAsync(id, cancellationToken);
        if (borrower == null)
            throw new NotFoundException("Borrower", id);
        await _repository.DeleteAsync(id, cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.ExistsAsync(id, cancellationToken);
    }
}
