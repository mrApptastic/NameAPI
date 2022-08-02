using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NameBandit.Data;
using NameBandit.Helpers;
using NameBandit.Models;

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
        [Route("Logs")]
        public async Task<ActionResult<ICollection<SyncLog>>> GetLog()
        {          
            return await _context.NameSyncLogs.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<string>> Sync()
        {          
            string msg = "";
  
            try {
                var names = from n in _context.Names select n;

                var theList = names.ToList();

                theList = ScrapingHelper.ScapeNames(theList, "http://www.urd.dk/fornavne/drenge.htm");

                theList = ScrapingHelper.ScapeNames(theList, "http://www.urd.dk/fornavne/piger.htm", true);

                // theList = ScapeNames2(theList);

                // AddCategories(_context);

                foreach (var item in theList.OrderBy(x => x.Text)) {
                    item.Vibration = CalculationHelper.CalculateNameVibration(item.Text);

                    if (item.Id == 0) {
                        _context.Names.Add(item);
                    } else {
                        var lilleIb = _context.Names.Where(x => x.Id == item.Id).FirstOrDefault();
                        if (lilleIb != null) {
                            lilleIb.Male = item.Male;
                            lilleIb.Female = item.Female;
                            lilleIb.Active = item.Active;
                            lilleIb.Text = item.Text;
                        }
                    }
                }

                msg = "The synchronization has succesfully been completed!";

            } catch (Exception exp) {
                msg = "The synchronization has failed with the following exception: " + exp.Message;
            }

            _context.NameSyncLogs.Add(new SyncLog() {
                Id = 0,
                Date = DateTime.Now,
                Log = msg
            });

            try {
                await _context.SaveChangesAsync();
            } catch (Exception saveExpt) {
                msg = "The synchronization has failed with the following exception: " + saveExpt.Message;
            }            

            return msg;
        }

        private static void AddCategories(ApplicationDbContext db) {

            string ib = System.IO.File.ReadAllText(@"c:\Temp\categories.json");
            List<Category> adder = JsonConvert.DeserializeObject<List<Category>>(ib);
   
            foreach (Category category in adder) {
                AddCategory(db, category.Title);

                var dbCategory = db.NameCategories.Where(x => x.Title == category.Title).FirstOrDefault();

                if (dbCategory != null) {
                    if (dbCategory.Names == null) {
                        dbCategory.Names = new List<Name>();
                    }
                    foreach (Name name in category.Names) {
                        var dbName = db.Names.Where(x => x.Text == StringFormatHelper.RemoveAccents(name.Text)).FirstOrDefault();

                        if (dbName != null) {
                            dbCategory.Names.Add(dbName);
                        }
                    }
                }

            }
        }

        private static void AddCategory(ApplicationDbContext db, string CategoryName) {
            bool hasCategory = db.NameCategories.Select(x => x.Title).Contains(CategoryName);

            if (!hasCategory) {
                db.NameCategories.Add(new Category() {
                    Id = 0,
                    Title = CategoryName,
                    Names = new List<Name>()
                });
            }
        }        
    }
}
