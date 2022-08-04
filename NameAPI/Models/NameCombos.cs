using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NameBandit.Models 
{
    public class NameCombo
    {  
        [Key]
        public int Id { get; set; }
        public ICollection<NamePrio> Names { get; set; }
        public Category Category { get; set; }
    }

    public class NamePrio {
        [Key]
        public int Id { get; set; }
        public Name Name { get; set; }
        public int? Prio { get; set; }
    }
}
