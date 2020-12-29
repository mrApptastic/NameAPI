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
using HtmlAgilityPack;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NameBandit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SyncController : ControllerBase
    {

        private readonly ILogger<SyncController> _logger;
        private readonly ApplicationDbContext _context;

        public SyncController(ILogger<SyncController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ActionResult<bool> Sync()
        {
            // WebClient w = new WebClient();
            // string boys = w.DownloadString("http://www.urd.dk/fornavne/drenge.htm");
            // string girls = w.DownloadString("http://www.urd.dk/fornavne/piger.htm");
            HtmlWeb web = new HtmlWeb();
            web.AutoDetectEncoding = false;
            web.OverrideEncoding = Encoding.GetEncoding("iso-8859-1");

            var boys = web.Load("http://www.urd.dk/fornavne/drenge.htm");

            var ib = boys.DocumentNode.Descendants("p").ToList().FindAll(x => x.InnerHtml.Contains("big"));
            // var girls = web.Load("http://www.urd.dk/fornavne/piger.htm");
            return true;
        }
    }
}
