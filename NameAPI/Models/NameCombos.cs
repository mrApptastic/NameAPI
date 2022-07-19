using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NameBandit.Models 
{
    public class NameCombos
    {  
        [Key]
        public int Id { get; set; }
        public ICollection<Name> Names { get; set; }
        public Category Category { get; set; }
    }  
}
