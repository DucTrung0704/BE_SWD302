using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Entities;
using Repositories.FluentApi;

namespace Repositories
{
    public class NuringServiceDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public NuringServiceDbContext(DbContextOptions<NuringServiceDbContext> options) : base(options) { }
        public NuringServiceDbContext() { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BackgroundDoctor> BackgroundDoctors { get; set; }

        public static string Getconnectionstring(string connectionstringname)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionstring = config.GetConnectionString(connectionstringname);
            return connectionstring;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
            => optionsbuilder.UseSqlServer(Getconnectionstring("defaultconnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BackgroundDoctorConfiguration());
        }
    }
}
