using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Google_Keep.Models
{
    public class Google_KeepContext : DbContext
    {
        public Google_KeepContext()
        {
        }

        public Google_KeepContext (DbContextOptions<Google_KeepContext> options)
            : base(options)
        {
        }

        public DbSet<Google_Keep.Models.Note> Note { get; set; }
    }
}
