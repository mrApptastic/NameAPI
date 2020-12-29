using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameBandit.Data
{
    public class ApplicationDbContext: DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
            { }

        public DbSet<Name> Names {get; set;}
    }
}   

