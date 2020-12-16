using EFFunkiness.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EFFunkiness.Server.Data
{
    public interface IEFFunkinessDbContext
    {
        DbSet<Client> Clients { get; }
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
