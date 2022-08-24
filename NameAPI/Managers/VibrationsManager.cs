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
	public interface IVibrationsManager {
        Task<ICollection<VibrationNumberViewModel>> GetAllVibrations();
	}

    public class VibrationsManager: IVibrationsManager
    {
        private readonly ILogger<VibrationsManager> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        
        public VibrationsManager(ILogger<VibrationsManager> logger, IMapper mapper, ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ICollection<VibrationNumberViewModel>> GetAllVibrations()
        {
            var vibrationList = await _context.NameVibrationNumbers.OrderBy(x => x.Vibration).ThenBy(x => x.Destiny).ToListAsync();
            
            return _mapper.Map<ICollection<VibrationNumber>, ICollection<VibrationNumberViewModel>>(vibrationList);
        }
    }
}
