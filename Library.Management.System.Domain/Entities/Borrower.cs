

namespace Library.Management.System.Domain.Entities;

public class Borrower:BaseEntity
{
    public string Name { get; private set; }= string.Empty;
    public string Email { get; private set; }= string.Empty;
    public string PhoneNumber { get; private set; }= string.Empty;
}
