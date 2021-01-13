using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using System.Web;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using Newtonsoft.Json;

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
        public async Task<ActionResult<string>> Sync()
        {          
            string msg = "";
  
            try {
                var names = from n in _context.Names select n;

                var theList = names.ToList();

                theList = ScapeNames(theList, "http://www.urd.dk/fornavne/drenge.htm");

                theList = ScapeNames(theList, "http://www.urd.dk/fornavne/piger.htm", true);

                theList = ScapeNames2(theList);

                AddCategories(_context);

                foreach (var item in theList.OrderBy(x => x.Text)) {
                    item.Vibration = CalculateNameVibration(item.Text);

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

            _context.SyncLogs.Add(new SyncLog() {
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
        
        private static List<Name> ScapeNames(List<Name> theList, string site, bool feminine = false) {
            HtmlWeb web = new HtmlWeb();
            web.AutoDetectEncoding = false;
            web.OverrideEncoding = Encoding.GetEncoding("iso-8859-1");

            var nameList = web.Load(site);
            var ib = nameList.DocumentNode.Descendants("p").ToList().FindAll(x => x.InnerHtml.Contains("</big>"));

            foreach (var bo in ib) {
                var per = bo.InnerHtml.Split("</big>")[1];

                if (per.Contains("</a>")) {
                    per = per.Split("</a>")[1];
                }

                if (per.Contains("<br>")) {
                    string[] persons = per.Split("<br>");

                    foreach (var mrMr in persons) {
                        string p = mrMr.Replace(@"\r\n","").Trim(); 

                        if (p.Length > 0 && !p.Contains("<a name=")) {
                            if (!theList.Select(x => x.Text).Contains(p)) {
                                theList.Add(new Name() {
                                    Id = 0,
                                    Text = p,
                                    Male = !feminine ? true : false, 
                                    Female = feminine ? true : false, 
                                    Active = true,
                                });
                            } else {
                                var uh = theList.Find(x => x.Text.Contains(p));
                                if (uh != null) {
                                    if (feminine) {
                                        uh.Female = true;
                                    } else {
                                        uh.Male = true;
                                    }
                                    uh.Active = true;
                                }
                            }
                        }
                    }
                }                
            }

            return theList;
        }

        private static List<Name> ScapeNames2(List<Name> theList) {
            // using (WebClient w = new WebClient()) {
            //     byte[] xls = w.DownloadData("https://familieretshuset.dk/media/1514/alle-godkendte-drengenavne-per-2020-12-09.xls");
            //     // System.IO.File.WriteAllBytes(@"c:\Temp\Test.xls", xls);

            //     using (MemoryStream uha = new MemoryStream(xls)) {
            //         using(ExcelPackage ep = new ExcelPackage(uha)) {
            //             ExcelWorksheet worksheet = ep.Workbook.Worksheets.FirstOrDefault();
            //         }
            //     }
            // }

            List<string> allNames = new List<string>();

            FileInfo boys = new FileInfo(@"c:\Temp\boys.xlsx");
            using(ExcelPackage ep = new ExcelPackage(boys)) {
                ExcelWorksheet worksheet = ep.Workbook.Worksheets.FirstOrDefault();
                if (worksheet != null) {
                    int rowCount = worksheet.Dimension.End.Row; 

                    for (int row = 1; row < rowCount; row++) {
                        string value = worksheet.Cells[row, 1].Value.ToString().Trim();

                        if (!theList.Select(x => x.Text).Contains(value)) {
                            theList.Add(new Name() {
                                Id = 0,
                                Text = value,
                                Male = true, 
                                Female = false,
                                Active = true,
                            });
                        } else {
                            var nm = theList.Find(x => x.Text.Contains(value));
                            if (nm != null) {
                                nm.Male = true;
                                nm.Active = true;
                            }
                        }

                        allNames.Add(value);
                    }
                    
                }
            }

            FileInfo girls = new FileInfo(@"c:\Temp\girls.xlsx");
            using(ExcelPackage ep = new ExcelPackage(girls)) {
                ExcelWorksheet worksheet = ep.Workbook.Worksheets.FirstOrDefault();
                if (worksheet != null) {
                    int rowCount = worksheet.Dimension.End.Row; 

                    for (int row = 1; row < rowCount; row++) {
                        string value = worksheet.Cells[row, 1].Value.ToString().Trim();

                        if (!theList.Select(x => x.Text).Contains(value)) {
                            theList.Add(new Name() {
                                Id = 0,
                                Text = value,
                                Male = false, 
                                Female = true,
                                Active = true,
                            });
                        } else {
                            var nm = theList.Find(x => x.Text.Contains(value));
                            if (nm != null) {
                                nm.Female = true;
                                nm.Active = true;
                            }
                        }

                        allNames.Add(value);
                    }
                    
                }
            }

            foreach (Name name in theList) {
                if (!allNames.Contains(name.Text)) {
                    name.Active = false;
                }
            }

            return theList;      
        }

        private static void AddCategories(ApplicationDbContext db) {

            string ib = System.IO.File.ReadAllText(@"c:\Temp\categories.json");
            List<Category> adder = JsonConvert.DeserializeObject<List<Category>>(ib);
   
            foreach (Category category in adder) {
                AddCategory(db, category.Title);

                var dbCategory = db.Categories.Where(x => x.Title == category.Title).FirstOrDefault();

                if (dbCategory != null) {
                    if (dbCategory.Names == null) {
                        dbCategory.Names = new List<Name>();
                    }
                    foreach (Name name in category.Names) {
                        var dbName = db.Names.Where(x => x.Text == StringBandit.RemoveAccents(name.Text)).FirstOrDefault();

                        if (dbName != null) {
                            dbCategory.Names.Add(dbName);
                        }
                    }
                }

            }
        }

        private static void AddCategory(ApplicationDbContext db, string CategoryName) {
            bool hasCategory = db.Categories.Select(x => x.Title).Contains(CategoryName);

            if (!hasCategory) {
                db.Categories.Add(new Category() {
                    Id = 0,
                    Title = CategoryName,
                    Names = new List<Name>()
                });
            }
        }        

        private static int CalculateNameVibration (string name) {
            int sum = 0;
            char[] arr = StringBandit.RemoveAccents(name).ToUpper().ToCharArray();

            foreach (char letter in arr) {
                sum += GetVibrationByLetter(letter);
            }

            return sum;
        }

        private static int GetVibrationByLetter (char letter) {
            switch (letter) {
                case 'A': return 1;
                case 'B': return 2;
                case 'C': return 3;
                case 'D': return 4;
                case 'E': return 5;
                case 'F': return 8;  
                case 'G': return 3;
                case 'H': return 5;
                case 'I': return 1;
                case 'J': return 1;
                case 'K': return 2;
                case 'L': return 3;
                case 'M': return 4;
                case 'N': return 5;
                case 'O': return 7;
                case 'P': return 8;
                case 'Q': return 1;
                case 'R': return 2;  
                case 'S': return 3;
                case 'T': return 4;
                case 'U': return 6;
                case 'V': return 6;
                case 'X': return 5;
                case 'Y': return 1;     
                case 'Z': return 7;
                case 'Æ': return 6;
                case 'Ø': return 7;
                case 'Å': return 1;                                   
                default: return  0;
            }
        }
    }

    static class StringBandit {
            public static string RemoveAccents(this string text){     
            StringBuilder sbReturn = new StringBuilder();     
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();  
            foreach (char letter in arrayText){     
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(letter) != System.Globalization.UnicodeCategory.NonSpacingMark)  
                    sbReturn.Append(letter);     
            }     
            return sbReturn.ToString();     
        } 
    }
}
