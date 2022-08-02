using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NameBandit.Models 
{
    public class Name
    {  
        [Key]
        public int Id {get; set; }
        public string Text { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public bool Active { get; set; }
        public bool Prefix { get; set; }
        public bool Suffix { get; set; }
        public int Vibration { get; set; }
#nullable enable
        public NameCombo? NameCombo { get; set; }
#nullable disable
    }
}
