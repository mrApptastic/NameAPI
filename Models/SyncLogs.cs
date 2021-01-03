using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SyncLog
   {  
       [Key]
       public int Id { get; set; }
       public DateTime Date { get; set; }
       public string Log { get; set; } 
   }  
