
namespace Library.Management.System.Domain.Entities;
public class Book : BaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public Guid AuthorId { get; private set; }=Guid.Empty;
    public Author? Author { get; set; }
    public string ISBN { get; private set; } = string.Empty;
    public DateTime PublishedDate { get; private set; }=DateTime.UtcNow;
}
