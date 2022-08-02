using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NameBandit.Models;

namespace NameBandit.Data
{
    public class ApplicationDbContext: DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
            { }

        public DbSet<Name> Names { get; set; }        
        public DbSet<Category> NameCategories { get; set; }
        public DbSet<NameCombo> NameCombinations { get; set; }        
        public DbSet<SyncLog> NameSyncLogs { get; set; }
    }
}   

