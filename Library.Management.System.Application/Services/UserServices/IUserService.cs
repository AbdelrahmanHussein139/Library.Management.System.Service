using Library.Management.System.Application.DTOs.UserDTOs;

namespace Library.Management.System.Application.Services.UserServices;

public interface IUserService
{
    public Task<UserDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<UserDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<UserDTO> CreateAsync(CreateUserDTO createDto, CancellationToken cancellationToken = default);
    public Task<UserDTO?> UpdateAsync(Guid id, UpdateUserDTO updateDto, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<UserDTO?> LoginAsync(SignInUserDTO signInUserDTO, CancellationToken cancellationToken = default);
    public Task<string?> GenerateJWTAsync(UserDTO userDto, CancellationToken cancellationToken = default);

}
