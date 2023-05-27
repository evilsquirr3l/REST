using Application.Models;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<CategoryDto> CreateCategoryAsync(BaseCategory baseCategory);
    Task<PagedResult<CategoryDto>> GetAllCategoriesAsync(int pageNumber, int pageSize);
    Task<CategoryDto?> GetCategoryByIdAsync(Guid id);
    Task<CategoryDto> UpdateCategoryAsync(BaseCategory categoryDto);
    Task DeleteCategoryAsync(CategoryDto categoryDto);
}
