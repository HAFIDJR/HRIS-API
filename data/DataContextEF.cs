using HRIS.models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace HRIS.data
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Job { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(_config.GetConnectionString(
                    "DefaultConnection"
                ),
                new MySqlServerVersion(new Version(8, 0, 30)),
                options => options.EnableRetryOnFailure()
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID);

            modelBuilder.Entity<Job>()
                .HasKey(u => u.JobID);

            modelBuilder.Entity<Job>()
                .HasMany(j => j.Employees)
                .WithOne(e => e.Job)
                .HasForeignKey(e => e.JobID);
        }
    }
}