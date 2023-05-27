using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ItemService : IItemService
{
    private readonly ICatalogDbContext _context;
    private readonly IMapper _mapper;

    public ItemService(ICatalogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ItemDto> CreateItemAsync(BaseItem baseItem)
    {
        var item = _mapper.Map<Item>(baseItem);
        
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ItemDto>(item);
    }

    public async Task<PagedResult<ItemDto>> GetAllItemsAsync(int pageNumber, int pageSize, string? categoryName = null)
    {
        var skip = (pageNumber - 1) * pageSize;

        var items = await _context.Items
            .Skip(skip)
            .Take(pageSize)
            .Include(x => x.Category)
            .Where(x => x.Category.Name == categoryName)
            .ToListAsync();

        var totalItems = await _context.Items.CountAsync();

        var itemDtos = _mapper.Map<List<ItemDto>>(items);

        return new PagedResult<ItemDto>
        {
            Data = itemDtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };
    }

    public async Task<ItemDto?> GetItemByIdAsync(Guid id)
    {
        var item = await _context.Items.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<ItemDto>(item);
    }

    public async Task<ItemDto> UpdateItemAsync(ItemDto itemDto)
    {
        var item = _mapper.Map<Item>(itemDto);

        _context.Items.Update(item);
        await _context.SaveChangesAsync();

        return _mapper.Map<ItemDto>(item);
    }

    public async Task DeleteItemAsync(ItemDto itemDto)
    {
        var item = _mapper.Map<Item>(itemDto);
        
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }
}

