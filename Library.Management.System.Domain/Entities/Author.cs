namespace Library.Management.System.Domain.Entities;

public class Author: BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Bio { get; private set; } =string.Empty;
}
