using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using NameBandit.Data;
using NameBandit.Models;
using AutoMapper;
using System;

namespace NameBandit.Managers
{
	public interface INameCombosManager {
        Task<ICollection<NameViewModel>> SuggestNameCombinations(int? category);
	}

    public class NameCombosManager: INameCombosManager
    {
        private readonly ILogger<NameCombosManager> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        
        public NameCombosManager(ILogger<NameCombosManager> logger, IMapper mapper, ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ICollection<NameViewModel>> SuggestNameCombinations(int? category)
        {
            var combos = _context.NameCombinations.AsQueryable();

            if (category > 0) {
                combos = combos.Where(x => x.Category.Id == category);
            }

            Random rand = new Random();

            int toSkip = rand.Next(1, await combos.CountAsync());

            var suggestion = await combos.Include(x => x.Names).ThenInclude(x => x.Name).ThenInclude(x => x.Vibration).Include(x => x.Category).Skip(toSkip).Take(1).FirstOrDefaultAsync();


            List<Name> nameList = suggestion.Names.OrderBy(x => x.Prio).Select(x => x.Name).ToList();

            return _mapper.Map<ICollection<Name>, ICollection<NameViewModel>>(nameList);
        }
    }
}
