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
    public class VibrationsController : ControllerBase
    {
        private readonly ILogger<VibrationsController> _logger;
        private readonly IVibrationsManager _manager;

        public VibrationsController(ILogger<VibrationsController> logger, IVibrationsManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<VibrationNumberViewModel>>> GetAll()
        {
            var vibrationList =  await _manager.GetAllVibrations();

            return Ok(vibrationList);
        }
    }
}
