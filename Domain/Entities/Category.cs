namespace Domain.Entities;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
    
    public ICollection<Item> Items { get; set; } = new List<Item>();
}
