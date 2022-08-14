using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using NameBandit.Data;
using NameBandit.Models;
using AutoMapper;

namespace NameBandit.Managers
{
	public interface INameCombosManager {

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


    }
}
