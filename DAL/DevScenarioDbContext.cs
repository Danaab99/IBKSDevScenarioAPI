using IBKSDevScenarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IBKSDevScenarioAPI.DAL
{
    public class DevScenarioDbContext : DbContext
    {
        public DevScenarioDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StatusLevel> StatusLevel { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Inquries> Inquries { get; set; }
    }
}
