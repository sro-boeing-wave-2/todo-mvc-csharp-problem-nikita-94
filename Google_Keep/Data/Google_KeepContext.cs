using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Google_Keep.Models;

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
        private readonly IMongoDatabase _database = null;

        public Google_KeepContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Note> Note
        {
            get
            {
                return _database.GetCollection<Note>("Note");
            }
        }
       // public DbSet<Google_Keep.Models.Note> Note { get; set; }
    }
}
