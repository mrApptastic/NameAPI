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
        public string IP { get; set; }
        public string Log { get; set; }
    }  
}
