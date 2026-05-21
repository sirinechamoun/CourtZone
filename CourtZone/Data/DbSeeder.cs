using CourtZone.Models;

namespace CourtZone.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Courts.Any()) return;

        var football = new Court
        {
            Name = "Champions Football Court",
            Sport = SportType.Football,
            PricePerHour = 40,
            Location = "Main Sports Complex",
            ImageUrl = "/images/football-placeholder.jpg"
        };

        var basketball = new Court
        {
            Name = "Hoop Arena Basketball Court",
            Sport = SportType.Basketball,
            PricePerHour = 25,
            Location = "City Indoor Stadium",
            ImageUrl = "/images/basketball-placeholder.jpg"
        };

        db.Courts.AddRange(football, basketball);
        db.SaveChanges();

        db.HappyHourDiscounts.AddRange(
            new HappyHourDiscount
            {
                CourtId = football.Id,
                DayOfWeek = DayOfWeek.Monday,
                StartMinute = 10 * 60,
                EndMinute = 14 * 60,
                DiscountPercentage = 30
            },
            new HappyHourDiscount
            {
                CourtId = basketball.Id,
                DayOfWeek = DayOfWeek.Tuesday,
                StartMinute = 11 * 60,
                EndMinute = 15 * 60,
                DiscountPercentage = 25
            }
        );
        db.SaveChanges();
    }
}
