using System.Web;
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
    public class NamesController : ControllerBase
    {
        private readonly ILogger<NamesController> _logger;
        private readonly INamesManager _manager;
        private readonly INameCombosManager _comboManager;

        public NamesController(ILogger<NamesController> logger, INamesManager manager, INameCombosManager comboManager)
        {
            _logger = logger;
            _manager = manager;
            _comboManager = comboManager;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<NameViewModel>>> Get(string matches, string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength, int? category, int page = 1, int take = 50)
        {
            var names =  await _manager.GetNames(HttpUtility.UrlDecode(matches), HttpUtility.UrlDecode(contains), HttpUtility.UrlDecode(startsWith), HttpUtility.UrlDecode(endsWith), sex, vib, maxLength, minLength, category, page = 1, take = 50);
            var nameList = names.results;

            // Response.Headers.Add("X-Count", names.count.ToString());

            return Ok(nameList);
        }

        [HttpGet("Suggest")]
        public async Task<ActionResult<ICollection<Name>>> Suggest(string matches, string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength, int? category, bool? title, bool? surname)
        {
            return Ok(await _manager.SuggestNames(HttpUtility.UrlDecode(matches), HttpUtility.UrlDecode(contains), HttpUtility.UrlDecode(startsWith), HttpUtility.UrlDecode(endsWith), sex, vib, maxLength, minLength, category, title, surname));
        }

        [HttpGet("SuggestCombo")]
        public async Task<ActionResult<ICollection<Name>>> SuggestCombo(int? category)
        {
            return Ok(await _comboManager.SuggestNameCombinations(category));
        }
    }
}
