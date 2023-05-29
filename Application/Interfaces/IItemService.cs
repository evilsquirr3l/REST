using Application.Models;

namespace Application.Interfaces;

public interface IItemService
{
    Task<ItemDto> CreateItemAsync(BaseItem baseItem);
    Task<PagedResult<ItemDto>> GetAllItemsAsync(int pageNumber, int pageSize, Guid? categoryId = null);
    Task<ItemDto?> GetItemByIdAsync(Guid id);
    Task<ItemDto> UpdateItemAsync(BaseItem baseItem);
    Task DeleteItemAsync(ItemDto itemDto);
}
