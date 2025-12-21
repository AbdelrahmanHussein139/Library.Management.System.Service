using Library.Management.System.Application.DTOs.UserDTOs;
using Library.Management.System.Application.Repositories;
using Library.Management.System.Application.Exceptions;
using MapsterMapper;
using Library.Management.System.Domain.Entities;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Library.Management.System.Application.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;


    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }

    public async Task<UserDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken);

        return user == null ? null : _mapper.Map<UserDTO>(user);
    }

    public async Task<List<UserDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<UserDTO>>(users);
    }

    public async Task<UserDTO> CreateAsync(CreateUserDTO createDto, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<User>(createDto);
        await _repository.AddAsync(user, cancellationToken);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO?> UpdateAsync(Guid id, UpdateUserDTO updateDto, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken);
        if (user == null)
            throw new NotFoundException("User", id);
        _mapper.Map(updateDto, user);
        await _repository.UpdateAsync(user, cancellationToken);
        return _mapper.Map<UserDTO>(user);
    }
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken);
        if (user == null)
            throw new NotFoundException("User", id);
        await _repository.DeleteAsync(id, cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.ExistsAsync(id, cancellationToken);
    }

    public async Task<UserDTO?> LoginAsync(SignInUserDTO signInDto, CancellationToken cancellationToken = default)
    {
        // Use repository method to get user by email
        var user = await _repository.GetByEmailAsync(signInDto.Email, cancellationToken);
        if (user == null || user.PasswordHash != signInDto.PasswordHash)
            throw new RuleException("Either email or password is incorrect");
        return _mapper.Map<UserDTO>(user);
    }
    public Task<string?> GenerateJWTAsync(UserDTO userDto, CancellationToken cancellationToken = default)
    {
        var key = Encoding.UTF8.GetBytes("SuperSecretKey12345SuperSecretKey12345");
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, userDto.Email),
        new Claim(ClaimTypes.Role, userDto.Role.ToString())
    };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult<string?>(tokenHandler.WriteToken(token));
    }
}
