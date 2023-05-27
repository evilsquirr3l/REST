namespace Application.Models;

public record ItemDto(Guid Id, string Name, string Description, CategoryDto Category);
