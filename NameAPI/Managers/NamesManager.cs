using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using NameBandit.Data;
using NameBandit.Models;
using NameBandit.Helpers;
using System.Collections;
using AutoMapper;

namespace NameBandit.Managers
{
	public interface INamesManager {
        Task<ICollection<NameViewModel>> GetNames(string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength);
	}

    public class NamesManager: INamesManager
    {
        private readonly ILogger<NamesManager> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        
        public NamesManager(ILogger<NamesManager> logger, IMapper mapper, ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ICollection<NameViewModel>> GetNames(string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength)
        {
            var names = _context.Names.Include(x => x.Vibration).AsQueryable();

            if (contains?.Length > 0) {
                names = names.Where(x => x.Text.ToLower().Contains(contains.ToLower()));
            }

            if (startsWith?.Length > 0) {
                names = names.Where(x => x.Text.ToLower().StartsWith(startsWith.ToLower()));
            }

            if (endsWith?.Length > 0) {
                names = names.Where(x => x.Text.ToLower().EndsWith(endsWith.ToLower()));
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

            if (sex?.ToLower() == "female") {
                    names = names.Where(x => x.Female == true);
            } else if (sex?.ToLower() == "male") {
                    names = names.Where(x => x.Male == true);
            } else if (sex?.ToLower() == "both") {
                    names = names.Where(x => x.Female == true && x.Male == true);
            }

            var nameList = await names.Where(x => x.Active).OrderBy(x => x.Text).ToListAsync();
            
            return _mapper.Map<ICollection<Name>, ICollection<NameViewModel>>(nameList);
        }

    }
}
