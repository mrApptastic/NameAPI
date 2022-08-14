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
	public interface ILogsManager {

	}

    public class LogsManager: ILogsManager
    {
        private readonly ILogger<LogsManager> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        
        public LogsManager(ILogger<LogsManager> logger, IMapper mapper, ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

    }
}
