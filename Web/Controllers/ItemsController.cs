using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }
    
    [HttpGet]
    public async Task<ActionResult<PagedResult<ItemDto>>> GetItems(int pageNumber = 1, int pageSize = 10, Guid? categoryId = null)
    {
        var items = await _itemService.GetAllItemsAsync(pageNumber, pageSize, categoryId);

        return Ok(items);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemById(Guid id)
    {
        var item = await _itemService.GetItemByIdAsync(id);

        if (item == null)
        {
            return NotFound();
        }
        
        return Ok(item);
    }
    
    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItem(BaseItem baseItem)
    {
        var item = await _itemService.CreateItemAsync(baseItem);

        return CreatedAtAction(nameof(CreateItem), new { id = item.Id }, item);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ItemDto>> UpdateItem(Guid id, ItemDto itemDto)
    {
        var item = await _itemService.GetItemByIdAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        var updatedItem = await _itemService.UpdateItemAsync(itemDto);

        return Ok(updatedItem);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem(Guid id)
    {
        var item = await _itemService.GetItemByIdAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        await _itemService.DeleteItemAsync(item);

        return NoContent();
    }
}
