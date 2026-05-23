using CourtZone.Data;
using CourtZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace CourtZone.Controllers;
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly AppDbContext _db;
    public AdminController(AppDbContext db) => _db = db; // private 

    public async Task<IActionResult> Dashboard()
    {
        ViewBag.TotalReservations = await _db.Reservations.CountAsync();
        ViewBag.PendingReservations = await _db.Reservations.CountAsync(r => r.Status == ReservationStatus.Pending);
        ViewBag.TotalRevenue = _db.Reservations.ToList().Sum(r => r.FinalPrice);
        ViewBag.Courts = await _db.Courts.CountAsync();

        return View(await _db.Reservations
            .Include(r => r.Court)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync());
    }

    public async Task<IActionResult> Courts() => View(await _db.Courts.ToListAsync());

    public IActionResult AddCourt() => View(new Court());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCourt(Court court)
    {
        if (!ModelState.IsValid) return View(court);

        _db.Courts.Add(court);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Courts));
    }

    public async Task<IActionResult> EditCourt(int id)
    {
        var court = await _db.Courts.FindAsync(id);
        if (court == null) return NotFound();

        return View(court);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCourt(Court court)
    {
        if (!ModelState.IsValid) return View(court);

        _db.Courts.Update(court);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Courts));
    }

    public async Task<IActionResult> Discounts()
    {
        var discounts = await _db.HappyHourDiscounts
            .Include(d => d.Court)
            .ToListAsync();

        return View(discounts);
    }

    public async Task<IActionResult> AddDiscount()
    {
        ViewBag.Courts = new SelectList(await _db.Courts.ToListAsync(), "Id", "Name");

        return View(new HappyHourDiscount
        {
            StartMinute = 10 * 60,
            EndMinute = 14 * 60,
            IsActive = true
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddDiscount(HappyHourDiscount discount)
    {
        if (discount.EndMinute <= discount.StartMinute)
            ModelState.AddModelError("", "End time must be after start time.");

        if (!ModelState.IsValid)
        {
            ViewBag.Courts = new SelectList(await _db.Courts.ToListAsync(), "Id", "Name");
            return View(discount);
        }

        _db.HappyHourDiscounts.Add(discount);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Discounts));
    }

    public async Task<IActionResult> EditDiscount(int id)
    {
        var discount = await _db.HappyHourDiscounts.FindAsync(id);
        if (discount == null) return NotFound();

        ViewBag.Courts = new SelectList(await _db.Courts.ToListAsync(), "Id", "Name");

        return View(discount);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditDiscount(HappyHourDiscount discount)
    {
        if (discount.EndMinute <= discount.StartMinute)
            ModelState.AddModelError("", "End time must be after start time.");

        if (!ModelState.IsValid)
        {
            ViewBag.Courts = new SelectList(await _db.Courts.ToListAsync(), "Id", "Name");
            return View(discount);
        }

        _db.HappyHourDiscounts.Update(discount);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Discounts));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteDiscount(int id)
    {
        var discount = await _db.HappyHourDiscounts.FindAsync(id);
        if (discount == null) return NotFound();

        _db.HappyHourDiscounts.Remove(discount);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Discounts));
    }

   public async Task<IActionResult> Approve(int id)
{
    var r = await _db.Reservations.FindAsync(id);

    if (r == null)
        return NotFound();

    r.Status = ReservationStatus.Approved;

    await _db.SaveChangesAsync();

    TempData["Success"] = "Reservation approved successfully!";

    return RedirectToAction(nameof(Dashboard));
}

   public async Task<IActionResult> Reject(int id)
{
    var r = await _db.Reservations.FindAsync(id);

    if (r == null)
        return NotFound();

    r.Status = ReservationStatus.Rejected;

    await _db.SaveChangesAsync();

    TempData["Error"] = "Reservation rejected.";

    return RedirectToAction(nameof(Dashboard));
}
}
