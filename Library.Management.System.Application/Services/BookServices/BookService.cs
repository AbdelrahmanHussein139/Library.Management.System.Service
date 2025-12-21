using Library.Management.System.Application.DTOs.BookDTOs;
using Library.Management.System.Application.Repositories;
using Library.Management.System.Application.Exceptions;
using MapsterMapper;
using Library.Management.System.Domain.Entities;

namespace Library.Management.System.Application.Services.BookServices;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;


    public BookService(IBookRepository repository, IAuthorRepository authorRepository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }

    public async Task<BookDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await _repository.GetByIdAsync(id, cancellationToken);

        return book == null ? null : _mapper.Map<BookDTO>(book);
    }

    public async Task<List<BookDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var books = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<BookDTO>>(books);
    }

    public async Task<BookDTO> CreateAsync(CreateBookDTO createDto, CancellationToken cancellationToken = default)
    {
        var book = _mapper.Map<Book>(createDto);
        var author = await _authorRepository.GetByIdAsync(book.AuthorId, cancellationToken);
        if (author == null)
            throw new NotFoundForeignKeyException("Author", book.AuthorId);
        await _repository.AddAsync(book, cancellationToken);
        return _mapper.Map<BookDTO>(book);
    }

    public async Task<BookDTO?> UpdateAsync(Guid id, UpdateBookDTO updateDto, CancellationToken cancellationToken = default)
    {
        var book = await _repository.GetByIdAsync(id, cancellationToken);
        if (book == null)
            throw new NotFoundException("Book", id);
        var author = await _authorRepository.GetByIdAsync(book.AuthorId, cancellationToken);
        if (author == null)
            throw new NotFoundForeignKeyException("Author", book.AuthorId);
        _mapper.Map(updateDto, book);
        await _repository.UpdateAsync(book, cancellationToken);
        return _mapper.Map<BookDTO>(book);
    }
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await _repository.GetByIdAsync(id, cancellationToken);
        if (book == null)
            throw new NotFoundException("Book", id);
        await _repository.DeleteAsync(id, cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.ExistsAsync(id, cancellationToken);
    }
}
