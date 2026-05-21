using CourtZone.Models;
using Microsoft.EntityFrameworkCore;

namespace CourtZone.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Court> Courts => Set<Court>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<HappyHourDiscount> HappyHourDiscounts => Set<HappyHourDiscount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Court>().Property(c => c.PricePerHour).HasPrecision(10, 2);
        modelBuilder.Entity<Reservation>().Property(r => r.OriginalPrice).HasPrecision(10, 2);
        modelBuilder.Entity<Reservation>().Property(r => r.DiscountAmount).HasPrecision(10, 2);
        modelBuilder.Entity<Reservation>().Property(r => r.FinalPrice).HasPrecision(10, 2);

        modelBuilder.Entity<Court>().HasData(
            new Court
            {
                Id = 1,
                Name = "Rebound Courts",
                Sport = SportType.Basketball,
                PricePerHour = 20,
                Location = "Jdeideh, Metn",
                Area = "Jdeideh",
                City = "Metn",
                DistanceFromBeirutKm = 8,
                ImageUrl = "https://images.unsplash.com/photo-1546519638-68e109498ffc?auto=format&fit=crop&w=900&q=80",
                IsActive = true
            },
            new Court
            {
                Id = 2,
                Name = "Rebound Mini Football",
                Sport = SportType.Football,
                PricePerHour = 35,
                Location = "Jdeideh, Metn",
                Area = "Jdeideh",
                City = "Metn",
                DistanceFromBeirutKm = 8,
                ImageUrl = "https://images.unsplash.com/photo-1575361204480-aadea25e6e68?auto=format&fit=crop&w=900&q=80",
                IsActive = true
            },
            new Court
            {
                Id = 3,
                Name = "GAME HOUSE Beirut Basketball",
                Sport = SportType.Basketball,
                PricePerHour = 25,
                Location = "Beirut",
                Area = "Beirut",
                City = "Beirut",
                DistanceFromBeirutKm = 3,
                ImageUrl = "https://images.unsplash.com/photo-1505666287802-931dc83948e9?auto=format&fit=crop&w=900&q=80",
                IsActive = true
            },
            new Court
            {
                Id = 4,
                Name = "GAME HOUSE Beirut Football",
                Sport = SportType.Football,
                PricePerHour = 30,
                Location = "Beirut",
                Area = "Beirut",
                City = "Beirut",
                DistanceFromBeirutKm = 3,
                ImageUrl = "https://images.unsplash.com/photo-1551958219-acbc608c6377?auto=format&fit=crop&w=900&q=80",
                IsActive = true
            },
            new Court
            {
                Id = 5,
                Name = "Batroun Village Club Basketball",
                Sport = SportType.Basketball,
                PricePerHour = 22,
                Location = "Batroun",
                Area = "Batroun",
                City = "Batroun",
                DistanceFromBeirutKm = 54,
                ImageUrl = "https://images.unsplash.com/photo-1519861531473-9200262188bf?auto=format&fit=crop&w=900&q=80",
                IsActive = true
            },
            new Court
            {
                Id = 6,
                Name = "Batroun Village Club Football",
                Sport = SportType.Football,
                PricePerHour = 28,
                Location = "Batroun",
                Area = "Batroun",
                City = "Batroun",
                DistanceFromBeirutKm = 54,
                ImageUrl = "https://images.unsplash.com/photo-1431324155629-1a6deb1dec8d?auto=format&fit=crop&w=900&q=80",
                IsActive = true
            },
            new Court
            {
                Id = 7,
                Name = "Fouad Chehab Stadium",
                Sport = SportType.Football,
                PricePerHour = 45,
                Location = "Jounieh",
                Area = "Jounieh",
                City = "Keserwan",
                DistanceFromBeirutKm = 20,
                ImageUrl = "https://images.unsplash.com/photo-1577223625816-7546f13df25d?auto=format&fit=crop&w=900&q=80",
                IsActive = true
            }
        );
    }
}