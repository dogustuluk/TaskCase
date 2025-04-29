namespace TaskCase.Domain.Entities;
public class Order : IEntity
{
    public int UserId { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
