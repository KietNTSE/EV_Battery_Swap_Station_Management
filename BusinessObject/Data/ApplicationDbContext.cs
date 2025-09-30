using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessObject.Enums;

namespace BusinessObject.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Add your DbSets here for other entities when you create them
        // public DbSet<Station> Stations { get; set; }
        // public DbSet<Battery> Batteries { get; set; }
        // public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure User entity
            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Role).HasConversion<int>().IsRequired();
                entity.Property(e => e.Status).HasConversion<int>().IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                // Create indexes for better performance
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Role);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.CreatedAt);
            });

            // Seed default roles if needed
            var adminRoleId = "1";
            var staffRoleId = "2";
            var customerRoleId = "3";

            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().HasData(
                new Microsoft.AspNetCore.Identity.IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new Microsoft.AspNetCore.Identity.IdentityRole
                {
                    Id = staffRoleId,
                    Name = "Staff",
                    NormalizedName = "STAFF"
                },
                new Microsoft.AspNetCore.Identity.IdentityRole
                {
                    Id = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
            );
        }
    }
}