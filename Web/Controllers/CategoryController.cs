using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<ActionResult<PagedResult<CategoryDto>>> GetCategories(int pageNumber = 1, int pageSize = 10)
    {
        var categories = await _categoryService.GetAllCategoriesAsync(pageNumber, pageSize);

        return Ok(categories);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }
        
        return Ok(category);
    }
    
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(BaseCategory baseCategory)
    {
        var category = await _categoryService.CreateCategoryAsync(baseCategory);

        return CreatedAtAction(nameof(CreateCategory), new { id = category.Id }, category);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> UpdateCategory(Guid id, BaseCategory baseCategory)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        var updatedCategory = await _categoryService.UpdateCategoryAsync(baseCategory);

        return Ok(updatedCategory);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        await _categoryService.DeleteCategoryAsync(category);

        return NoContent();
    }
}
