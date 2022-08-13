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
        Task<(ICollection<NameViewModel> results, int count)> GetNames(string matches, string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength, int? category, int page = 1, int take = 50);
        Task<ICollection<NameViewModel>> SuggestNames(string matches, string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength, int? category, bool? title = null, bool? surname = null);
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

        public async Task<(ICollection<NameViewModel> results, int count)> GetNames(string matches, string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength, int? category, int page = 1, int take = 50)
        {
            IQueryable<Name> names = ApplyNameFilter(_context.Names.Include(x => x.Vibration).Include(x => x.Category).AsQueryable(), matches, contains, startsWith, endsWith, sex, vib, maxLength, minLength, category);

            int count = await names.CountAsync();

            var nameList = await names.Where(x => x.Active).OrderBy(x => x.Text).Skip((page -1) * take).Take(take).ToListAsync();
            
            return (_mapper.Map<ICollection<Name>, ICollection<NameViewModel>>(nameList), count);
        }

        public async Task<ICollection<NameViewModel>> SuggestNames(string matches, string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength, int? category, bool? title = null, bool? surname = null)
        {
            Name name = await Generate(matches, contains, startsWith, endsWith, sex, vib, maxLength, minLength, category);

            List<Name> nameList = new List<Name>() { name };

            if (title == true) {
                Name prefix = await Generate(null, null, null, null, null, null, null, null, null, true);

                nameList.Insert(0, prefix);
            }

            if (title == true) {
                Name suffix = await Generate(null, null, null, null, null, null, null, null, null, null, true);

                nameList.Add(suffix);
            }

            return _mapper.Map<ICollection<Name>, ICollection<NameViewModel>>(nameList);
        }


        private async Task<Name> Generate(string matches, string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength, int? category, bool? title = null, bool? surname = null) {
            Random rand = new Random();

            IQueryable<Name> names = ApplyNameFilter(_context.Names.Include(x => x.Vibration).Include(x => x.Category).AsQueryable(), matches, contains, startsWith, endsWith, sex, vib, maxLength, minLength, category, title, surname);

            int toSkip = rand.Next(1, await names.CountAsync());

            return await names.Skip(toSkip).Take(1).FirstOrDefaultAsync();
        }

        private IQueryable<Name> ApplyNameFilter(IQueryable<Name> names, string matches, string contains, string startsWith, string endsWith, string sex, int? vib, int? maxLength, int? minLength, int? category, bool? title = null, bool? surname = null) {
                        
            if (matches?.Length > 0) {
                names = names.Where(x => x.Text.ToLower() == matches.ToLower());
            }

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

           if (category > 0) {
                names = names.Where(x => x.Category.Id == category);
           }

           if (title == true) {
                names = names.Where(x => x.Prefix);
           }

           if (surname == true) {
                names = names.Where(x => x.Suffix);
           }

           return names;            
        }
    }
}
