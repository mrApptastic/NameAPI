using System;
using System.Collections.Generic;

namespace NameBandit.Models 
{
    public class SearchObj
    {  
        public ICollection<int> Categories {get; set; }
        public string Title { get; set; }
        public ICollection<int> VibrationNumbers { get; set; }
    }
}
