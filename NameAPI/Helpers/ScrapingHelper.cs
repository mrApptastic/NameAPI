using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;
using HtmlAgilityPack;
using System.Text;
using OfficeOpenXml;
using NameBandit.Models;
// https://min-mave.dk/navne/[Name] // http://www.navnebetydning.dk/drengenavn/[Name].shtml/
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
    }

}
