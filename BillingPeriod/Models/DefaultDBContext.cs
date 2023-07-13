using Microsoft.EntityFrameworkCore;

namespace BillingPeriod.Models
{
    public class DefaultDBContext : DbContext
    {
        public DefaultDBContext()
        {
        }
        public DefaultDBContext(DbContextOptions<DefaultDBContext> options)
            : base(options)
        {
        }

        public DbSet<Activity> Activity { get; set; }
        public DbSet<Note> Note { get; set; }

        // DB Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
