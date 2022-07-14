using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class VibrationNumber
   {  
       [Key]
       public int Id {get; set; }
       public int Vibration { get; set; }
       public int Destiny { get; set; }
       public string Title { get; set; }
       public string Description { get; set; }
   }  
