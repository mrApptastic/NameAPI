using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NameBandit.Models;
using NameBandit.Managers;

namespace NameBandit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoriesManager _manager;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoriesManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CategoryViewModel>>> GetAll()
        {
            var categoryList =  await _manager.GetAllCategories();

            return Ok(categoryList);
        }
    }
}
