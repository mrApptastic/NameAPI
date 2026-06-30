using System.Web;
using Microsoft.AspNetCore.Mvc;
using NameBandit.Models;
using NameBandit.Managers;

namespace NameBandit.Controllers;

[ApiController]
[Route("[controller]")]
public class NamesController(INamesManager manager, INameCombosManager comboManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<NameViewModel>>> Get(
        string? matches, string? contains, string? startsWith, string? endsWith,
        string? sex, int? vib, int? maxLength, int? minLength, int? category,
        int page = 1, int take = 50)
    {
        var names = await manager.GetNames(
            HttpUtility.UrlDecode(matches), HttpUtility.UrlDecode(contains),
            HttpUtility.UrlDecode(startsWith), HttpUtility.UrlDecode(endsWith),
            sex, vib, maxLength, minLength, category, page, take);

        Response.Headers["X-Count"] = names.count.ToString();

        return Ok(names.results);
    }

    [HttpGet("Suggest")]
    public async Task<ActionResult<ICollection<NameViewModel>>> Suggest(
        string? matches, string? contains, string? startsWith, string? endsWith,
        string? sex, int? vib, int? maxLength, int? minLength, int? category,
        bool? title, bool? surname)
    {
        return Ok(await manager.SuggestNames(
            HttpUtility.UrlDecode(matches), HttpUtility.UrlDecode(contains),
            HttpUtility.UrlDecode(startsWith), HttpUtility.UrlDecode(endsWith),
            sex, vib, maxLength, minLength, category, title, surname));
    }

    [HttpGet("SuggestCombo")]
    public async Task<ActionResult<ICollection<NameViewModel>>> SuggestCombo(int? category)
    {
        return Ok(await comboManager.SuggestNameCombinations(category));
    }
}
