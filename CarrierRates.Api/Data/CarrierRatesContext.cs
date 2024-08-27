using System;
using CarrierRates.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarrierRates.Api.Data;

public class CarrierRatesContext(DbContextOptions<CarrierRatesContext> options) : DbContext(options)
{
    public DbSet<Carrier> Carriers => Set<Carrier>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrier>().HasData(
            new Carrier() { Id = 1, Name = "DHL", isEnabled = true, UpdatedAt = DateOnly.FromDateTime(DateTime.Now) },
            new Carrier() { Id = 2, Name = "Lalamove", isEnabled = true, UpdatedAt = DateOnly.FromDateTime(DateTime.Now) }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=CarrierRates.db");
    }
}
