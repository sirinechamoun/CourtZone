using CourtZone.Data;
using CourtZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourtZone.Controllers;

public class ReservationsController : Controller
{
    private readonly AppDbContext _db;
    public ReservationsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Create(int courtId)
    {
        var court = await _db.Courts
            .Include(c => c.HappyHourDiscounts)
            .FirstOrDefaultAsync(c => c.Id == courtId);

        if (court == null) return NotFound();

        var bestDiscount = court.HappyHourDiscounts
            .Where(h => h.IsActive)
            .OrderByDescending(h => h.DiscountPercentage)
            .FirstOrDefault();

        ViewBag.Court = court;
        ViewBag.BestDiscount = bestDiscount;

        return View(new Reservation
        {
            CourtId = courtId,
            StartTime = DateTime.Today.AddDays(1).AddHours(10),
            DurationHours = 1.5
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Reservation reservation)
    {
        var court = await _db.Courts
            .Include(c => c.HappyHourDiscounts)
            .FirstOrDefaultAsync(c => c.Id == reservation.CourtId);

        if (court == null) return NotFound();

        var bestDiscount = court.HappyHourDiscounts
            .Where(h => h.IsActive)
            .OrderByDescending(h => h.DiscountPercentage)
            .FirstOrDefault();

        if (reservation.StartTime <= DateTime.Now)
            ModelState.AddModelError(nameof(reservation.StartTime), "Reservation time must be in the future.");

        bool doubleBooked = await _db.Reservations.AnyAsync(r =>
            r.CourtId == reservation.CourtId &&
            r.Status != ReservationStatus.Cancelled &&
            r.Status != ReservationStatus.Rejected &&
            reservation.StartTime < r.StartTime.AddHours(r.DurationHours) &&
            reservation.StartTime.AddHours(reservation.DurationHours) > r.StartTime);

        if (doubleBooked)
            ModelState.AddModelError("", "This court is already reserved during that time.");

        if (!ModelState.IsValid)
        {
            ViewBag.Court = court;
            ViewBag.BestDiscount = bestDiscount;
            return View(reservation);
        }

        reservation.OriginalPrice = court.PricePerHour * (decimal)reservation.DurationHours;

        var discount = FindHappyHourDiscount(court, reservation.StartTime);

        reservation.DiscountAmount = discount == null
            ? 0
            : reservation.OriginalPrice * discount.DiscountPercentage / 100m;

        reservation.FinalPrice = reservation.OriginalPrice - reservation.DiscountAmount;
        reservation.Status = ReservationStatus.Pending;
        reservation.CreatedAt = DateTime.Now;

        _db.Reservations.Add(reservation);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Confirmation), new { id = reservation.Id });
    }

    public async Task<IActionResult> Confirmation(int id)
    {
        var reservation = await _db.Reservations
            .Include(r => r.Court)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reservation == null) return NotFound();

        return View(reservation);
    }

    private static HappyHourDiscount? FindHappyHourDiscount(Court court, DateTime startTime)
    {
        var startMinute = startTime.Hour * 60 + startTime.Minute;

        return court.HappyHourDiscounts.FirstOrDefault(d =>
            d.IsActive &&
            d.DayOfWeek == startTime.DayOfWeek &&
            startMinute >= d.StartMinute &&
            startMinute < d.EndMinute);
    }public async Task<IActionResult> MyReservations(string email)
{
    if (string.IsNullOrWhiteSpace(email))
        return View(new List<Reservation>());

    var reservations = await _db.Reservations
        .Include(r => r.Court)
        .Where(r => r.CustomerEmail == email)
        .OrderByDescending(r => r.CreatedAt)
        .ToListAsync();

    ViewBag.Email = email;

    return View(reservations);
}
}