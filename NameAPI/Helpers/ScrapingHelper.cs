using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;
using HtmlAgilityPack;
using System.Text;
using OfficeOpenXml;
using NameBandit.Models;

namespace NameBandit.Helpers
{
    public class ScrapingHelper
    {       
        public static List<Name> ScapeNames(List<Name> theList, string site, bool feminine = false) {
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

        public static List<Name> ScapeNames2(List<Name> theList) {
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
 
        public static string AddNameData(Name name) {
            HtmlWeb web = new HtmlWeb();
            web.AutoDetectEncoding = false;
            web.OverrideEncoding = Encoding.GetEncoding("iso-8859-1");

            // https://min-mave.dk/navne/[Name.ToLower()] 
            // http://www.navnebetydning.dk/drengenavn/[Name].shtml/ 

            string url = String.Format("http://www.navnebetydning.dk/{0}/{1}.shtml", (name.Male ? "drengenavn" : "pigenavn"), name?.Text);
            var ib = web.Load(url);

            var bo = ib.DocumentNode.SelectNodes("//table");

            if (bo != null) {
                foreach (HtmlNode row in bo) {
                    foreach (HtmlAttribute attr in row.Attributes) {
                        if (attr?.Name == "width") {
                            if (attr?.Value == "100%") {
                                if (row.InnerText.Contains("Se også:")) {
                                    return row.InnerText.Substring(row.InnerText.IndexOf(name.Text) + name.Text.Length).Split("Se også:")[0].Replace(System.Environment.NewLine, " ").Replace("&nbsp;", " ").Replace(".", ". ").Trim();
                                }                            
                            }
                        }
                    }
                }
            }
           
            return "";
        }

        public static List<VibrationNumber> GetVibrationMeanings() {
            HtmlWeb web = new HtmlWeb();
            web.AutoDetectEncoding = true;
            web.OverrideEncoding = Encoding.UTF8;

            var bobby = new List<VibrationNumber>();

            string url = "https://hellasofia.com/numerologisk-navne-beregner/";
            var ib = web.Load(url);

            HtmlNodeCollection abra = ib.DocumentNode.SelectNodes("//h6[contains(@class, 'et_pb_toggle_title')]");

            HtmlNodeCollection kadabra = ib.DocumentNode.SelectNodes("//div[contains(@class, 'et_pb_toggle_content')]");

            for (int j = 1; j < 10; j++) {
                bobby.Add(new VibrationNumber() {
                    Id = 0,
                    Vibration = j,
                    Destiny = 0,
                    Title = abra[0].InnerText,
                    Description = kadabra[0].InnerText
                });
            }

            for (int i = 1; i < abra.Count(); i++) {
                string titleArea = abra[i].InnerText.Replace("–", "-");

                if (!titleArea.Contains(" - ")) {
                    continue;
                }
                
                int vibration = int.Parse(titleArea.Split(" - ")[0].Split("/")[0]);
                int destiny = 0;

                if (titleArea.Split(" - ")[0].Contains("/")) {
                    destiny = int.Parse(titleArea.Split(" - ")[0].Split("/")[1]);
                }

                string title = titleArea.Split(" - ")[1];
                string description = kadabra[i].InnerText;

                bobby.Add(new VibrationNumber() {
                    Id = 0,
                    Vibration = vibration,
                    Destiny = destiny,
                    Title = title,
                    Description = description
                });
            }
          
            return bobby;
        }
    }
}
