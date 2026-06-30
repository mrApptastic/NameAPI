using Microsoft.EntityFrameworkCore;
using NameBandit.Models;

namespace NameBandit.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Name> Names { get; set; }
    public DbSet<Category> NameCategories { get; set; }
    public DbSet<NameCombo> NameCombinations { get; set; }
    public DbSet<SearchLog> NameSearchLogs { get; set; }
    public DbSet<SyncLog> NameSyncLogs { get; set; }
    public DbSet<VibrationNumber> NameVibrationNumbers { get; set; }
}