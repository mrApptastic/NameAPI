using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Suggestion
   {  
       [Key]
       public int Id {get; set; }
       public string Name { get; set; }
       public string Category { get; set; }
   }  
