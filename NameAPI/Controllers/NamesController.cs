using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NameBandit.Data;
using NameBandit.Models;

namespace NameBandit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NamesController : ControllerBase
    {

        private readonly ILogger<NamesController> _logger;
        private readonly ApplicationDbContext _context;

        public NamesController(ILogger<NamesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Name>>> Get(string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength)
        {
            var names = _context.Names.Include(x => x.Vibration).AsQueryable();

            if (contains?.Length > 0) {
                names = names.Where(x => x.Text.ToLower().Contains(HttpUtility.UrlDecode(contains).ToLower()));
            }

            if (startsWith?.Length > 0) {
                names = names.Where(x => x.Text.ToLower().StartsWith(HttpUtility.UrlDecode(startsWith).ToLower()));
            }

            if (endsWith?.Length > 0) {
                names = names.Where(x => x.Text.ToLower().EndsWith(HttpUtility.UrlDecode(endsWith).ToLower()));
            }

            if (vib > 0) {
                names = names.Where(x => x.Vibration.Vibration == vib);
            }

            if (maxLength > 0) {
                names = names.Where(x => x.Text.Length <= maxLength);
            }

            if (minLength > 0) {
                names = names.Where(x => x.Text.Length >= minLength);
            }

            if (sex.ToLower() == "female") {
                    names = names.Where(x => x.Female == true);
            } else if (sex.ToLower() == "male") {
                    names = names.Where(x => x.Male == true);
            } else if (sex.ToLower() == "both") {
                    names = names.Where(x => x.Female == true && x.Male == true);
            }
            
            return Ok(await names.Where(x => x.Active).OrderBy(x => x.Text).ToListAsync());
        }

        [HttpGet("suggest")]
        public async Task<ActionResult<IEnumerable<Name>>> Suggest(int? vib, int? sex)
        {
            var names = _context.Names.Include(x => x.Vibration).AsQueryable();

            if (vib > 0) {
                names = names.Where(x => x.Vibration.Vibration == vib);
            }

            if (sex != null) {
                if (sex == 1) {
                    names = names.Where(x => x.Female == true);
                } else if (sex == 0) {
                    names = names.Where(x => x.Male == true);
                }
            }
            
            return Ok(await names.Where(x => x.Active).OrderBy(x => x.Text).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Name>>> Search(Name name)
        {

            var names = from n in _context.Names where n.Id == name.Id select n;
            
            return Ok(await names.ToListAsync());
        }

        private async Task<Name> Generate() {
            Random rand = new Random();
            int toSkip = rand.Next(1, await _context.Names.CountAsync());
            return await _context.Names.Skip(toSkip).Take(1).FirstOrDefaultAsync();
        }
    }
}
