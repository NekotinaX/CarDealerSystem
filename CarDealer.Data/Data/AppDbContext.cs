using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Sale> Sales => Set<Sale>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(c => c.Brand)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Price)
                    .HasColumnType("decimal(18,2)");

                entity.Property(c => c.FuelType)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(c => c.Email)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(s => s.FinalPrice)
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(s => s.Car)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CarId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Customer)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}