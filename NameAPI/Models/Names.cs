using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Name
   {  
       [Key]
       public int Id {get; set; }
       public string Text { get; set; }
       public bool Male { get; set; }
       public bool Female { get; set; }
       public bool Active { get; set; }
       public int Vibration { get; set; }
   }  
