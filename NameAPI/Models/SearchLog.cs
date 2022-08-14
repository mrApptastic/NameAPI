using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NameBandit.Models 
{
    public class SearchLog
    {  
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string IPAdress { get; set; }
        public string Matches { get; set; } 
        public string Contains { get; set; }
        public string StartsWith { get; set; }
        public string EndsWith { get; set; }
        public string Gender { get; set; }
        public int? VibrationNumber { get; set; }
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public int? Category { get; set; }
        public bool? Title { get; set; }
        public bool? Surname { get; set; }
    }  
}
