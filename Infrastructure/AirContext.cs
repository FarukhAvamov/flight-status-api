using Domain.Domain.SeedWork;
using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;
using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class AirContext : DbContext,IUnitOfWork
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        private string? DbPath { get; }
        
        public AirContext(DbContextOptions<AirContext> options) : base(options) { }

    
        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        // {
        //     options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        //     // options.EnableSensitiveDataLogging();
        // }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(p => p.ID);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Role>().HasKey(p => p.ID);
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Code)
                .IsUnique();
            modelBuilder.Entity<Role>().HasData(
                new Role{ ID = 1, Code = "user" },
                new Role{ ID = 2, Code = "moderator"}
            );
        }
        
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            _ = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
