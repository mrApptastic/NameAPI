using System.ComponentModel.DataAnnotations;

namespace NameBandit.Models;

public class SyncLog
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Log { get; set; } = string.Empty;
}
