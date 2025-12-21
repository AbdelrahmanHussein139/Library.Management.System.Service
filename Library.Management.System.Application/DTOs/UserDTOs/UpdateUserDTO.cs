using Library.Management.System.Domain.Enums;

namespace Library.Management.System.Application.DTOs.UserDTOs;

public class UpdateUserDTO
{
    public string FirstName { get;  set; } = string.Empty;
    public string LastName { get;  set; } = string.Empty;
    public string Email { get;  set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public LibrarySystemRole Role { get;  set; } = LibrarySystemRole.Guest;// e.g., Admin,Member,Guest
}
