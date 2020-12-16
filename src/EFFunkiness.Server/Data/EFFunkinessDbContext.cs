using EFFunkiness.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFFunkiness.Server.Data
{
    public class EFFunkinessDbContext: DbContext, IEFFunkinessDbContext
    {
        public EFFunkinessDbContext(DbContextOptions options)
            :base(options) { }

        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Client> Clients { get; private set; }
        public DbSet<User> Users { get; private set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
