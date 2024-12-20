using Entities;
using Microsoft.EntityFrameworkCore;

namespace SEP3_T3_ASP_Core_WebAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; } // Table for storing Item entities
        public DbSet<Order> Orders { get; set; } // Table for storing Order entities
        public DbSet<User> Users { get; set; } // Table for storing User entities
        public DbSet<OrderItem> OrderItems { get; set; } // Table for storing OrderItem entities

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // Avoid configuring twice
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=sep3db;Username=postgres;Password=P@ssw0rd;Timeout=10;SSL Mode=Prefer");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ItemId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Delete on cascade for OrderItem when Order is deleted

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(oi => oi.ItemId)
                .OnDelete(DeleteBehavior.Cascade); // Delete on cascade for OrderItem when Item is deleted

            modelBuilder.Entity<Order>()
                .HasOne(o => o.AssignedUser)
                .WithMany(u => u.Orders) // Added this relationship to allow User to access Orders
                .HasForeignKey(o => o.UserId)
    .OnDelete(DeleteBehavior.Cascade); // Change from Restrict to Cascade

            modelBuilder.Entity<Order>()
                .HasOne(o => o.CreatedBy)
                .WithMany()
                .HasForeignKey(o => o.CreatedById)
    .OnDelete(DeleteBehavior.Cascade); // Change from Restrict to Cascade

            modelBuilder.Entity<Item>()
                .HasMany(i => i.OrderItems)
                .WithOne(oi => oi.Item)
                .HasForeignKey(oi => oi.ItemId)
                .OnDelete(DeleteBehavior.Cascade); // Delete on cascade for OrderItem when Item is deleted

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Delete on cascade for OrderItem when Order is deleted

            modelBuilder.Entity<User>()
                .HasMany<Order>(u => u.Orders) // Added missing Orders navigation property in User entity
                .WithOne(o => o.AssignedUser)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Delete on cascade for Orders when User is deleted
        }
    }
}
