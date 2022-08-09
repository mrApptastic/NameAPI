﻿using System;
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
using NameBandit.Managers;

namespace NameBandit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NamesController : ControllerBase
    {
        private readonly ILogger<NamesController> _logger;
        private readonly INamesManager _manager;

        public NamesController(ILogger<NamesController> logger, INamesManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<NameViewModel>>> Get(string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength)
        {
            var nameList = await _manager.GetNames(HttpUtility.UrlDecode(contains), HttpUtility.UrlDecode(startsWith), HttpUtility.UrlDecode(endsWith), sex, vib, maxLength, minLength);

            return Ok(nameList);
        }

        // [HttpGet("suggest")]
        // public async Task<ActionResult<IEnumerable<Name>>> Suggest(int? vib, int? sex)
        // {
        //     var names = _context.Names.Include(x => x.Vibration).AsQueryable();

        //     if (vib > 0) {
        //         names = names.Where(x => x.Vibration.Vibration == vib);
        //     }

        //     if (sex != null) {
        //         if (sex == 1) {
        //             names = names.Where(x => x.Female == true);
        //         } else if (sex == 0) {
        //             names = names.Where(x => x.Male == true);
        //         }
        //     }
            
        //     return Ok(await names.Where(x => x.Active).OrderBy(x => x.Text).ToListAsync());
        // }

        // [HttpPost]
        // public async Task<ActionResult<IEnumerable<Name>>> Search(Name name)
        // {

        //     var names = from n in _context.Names where n.Id == name.Id select n;
            
        //     return Ok(await names.ToListAsync());
        // }

        // private async Task<Name> Generate() {
        //     Random rand = new Random();
        //     int toSkip = rand.Next(1, await _context.Names.CountAsync());
        //     return await _context.Names.Skip(toSkip).Take(1).FirstOrDefaultAsync();
        // }
    }
}
