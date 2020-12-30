using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
   {  
       [Key]
       public int Id {get; set; }
       public string Title { get; set; }
       public ICollection<Name> Names { get; set; }
   }  
