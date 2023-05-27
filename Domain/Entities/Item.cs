namespace Domain.Entities;

public class Item
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;

    public Category Category { get; set; } = new();

    public Guid CategoryId { get; set; }
}
