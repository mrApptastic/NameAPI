using Microsoft.AspNetCore.Mvc;
using NameBandit.Models;
using NameBandit.Managers;

namespace NameBandit.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController(ICategoriesManager manager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<CategoryViewModel>>> GetAll()
    {
        var categoryList = await manager.GetAllCategories();
        return Ok(categoryList);
    }
}
