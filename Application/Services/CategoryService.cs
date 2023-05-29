using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICatalogDbContext _context;
    private readonly IMapper _mapper;

    public CategoryService(ICatalogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryDto> CreateCategoryAsync(BaseCategory baseCategory)
    {
        var category = _mapper.Map<Category>(baseCategory);

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<PagedResult<CategoryDto>> GetAllCategoriesAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;

        var categories = await _context.Categories
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var totalItems = await _context.Categories.CountAsync();

        var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

        return new PagedResult<CategoryDto>
        {
            Data = categoriesDto,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(Guid id)
    {
        var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateCategoryAsync(BaseCategory categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task DeleteCategoryAsync(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryWithItemsAsync(CategoryDto categoryDto)
    {
        var items = _context.Items.Where(x => x.Category.Id == categoryDto.Id);
        _context.Items.RemoveRange(items);
        
        await DeleteCategoryAsync(categoryDto);
    }
}
