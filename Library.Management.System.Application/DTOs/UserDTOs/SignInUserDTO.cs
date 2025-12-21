

namespace Library.Management.System.Application.DTOs.UserDTOs;

public class SignInUserDTO
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
