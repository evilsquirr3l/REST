using Application.Models;

namespace Application.Interfaces;

public interface IItemService
{
    Task<ItemDto> CreateItemAsync(BaseItem baseItem);
    Task<PagedResult<ItemDto>> GetAllItemsAsync(int pageNumber, int pageSize, string? categoryName = null);
    Task<ItemDto?> GetItemByIdAsync(Guid id);
    Task<ItemDto> UpdateItemAsync(ItemDto itemDto);
    Task DeleteItemAsync(ItemDto itemDto);
}
