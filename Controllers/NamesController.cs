using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NameBandit.Data;

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
        public async Task<ActionResult<IEnumerable<Name>>> Get()
        {
            var names = from n in _context.Names select n;
            
            return await names.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Name>>> Search(Name name)
        {

            var names = from n in _context.Names where n.Id == name.Id select n;
            
            return await names.ToListAsync();
        }
    }
}
