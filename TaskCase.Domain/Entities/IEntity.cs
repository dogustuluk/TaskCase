namespace TaskCase.Domain.Entities;
public class IEntity
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
