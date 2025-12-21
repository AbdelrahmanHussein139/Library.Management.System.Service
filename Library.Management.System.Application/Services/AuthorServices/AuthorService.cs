using Library.Management.System.Application.DTOs.AuthorDTOs;
using Library.Management.System.Application.Repositories;
using Library.Management.System.Application.Exceptions;
using MapsterMapper;
using Library.Management.System.Domain.Entities;

namespace Library.Management.System.Application.Services.AuthorServices;

public class AuthorService: IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;


    public AuthorService(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }

    public async Task<AuthorDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await _repository.GetByIdAsync(id, cancellationToken);

        return author == null ? null : _mapper.Map<AuthorDTO>(author);
    }

    public async Task<List<AuthorDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var opportunities = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<AuthorDTO>>(opportunities);
    }

    public async Task<AuthorDTO> CreateAsync(CreateAuthorDTO createDto, CancellationToken cancellationToken = default)
    {
        var author = _mapper.Map<Author>(createDto);
        await _repository.AddAsync(author, cancellationToken);
        return _mapper.Map<AuthorDTO>(author);
    }

    public async Task<AuthorDTO?> UpdateAsync(Guid id, UpdateAuthorDTO updateDto, CancellationToken cancellationToken = default)
    {
        var author =  await _repository.GetByIdAsync(id, cancellationToken);
        if (author == null)
            throw new NotFoundException("Author", id);
        _mapper.Map(updateDto,author);
        await _repository.UpdateAsync(author, cancellationToken);
        return _mapper.Map<AuthorDTO>(author);
    }
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await _repository.GetByIdAsync(id, cancellationToken);
        if (author == null)
            throw new NotFoundException("Author", id);
        await _repository.DeleteAsync(id, cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.ExistsAsync(id, cancellationToken);
    }
}
