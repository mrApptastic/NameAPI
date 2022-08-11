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
	public interface ICategoriesManager {
        Task<ICollection<CategoryViewModel>> GetAllCategories();
	}

    public class CategoriesManager: ICategoriesManager
    {
        private readonly ILogger<CategoriesManager> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        
        public CategoriesManager(ILogger<CategoriesManager> logger, IMapper mapper, ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ICollection<CategoryViewModel>> GetAllCategories()
        {
            var categoryList = await _context.NameCategories.OrderBy(x => x.Title).ToListAsync();
            
            return _mapper.Map<ICollection<Category>, ICollection<CategoryViewModel>>(categoryList);
        }

    }
}
