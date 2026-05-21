using CourtZone.Data;
using CourtZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourtZone.Controllers;

public class CourtsController : Controller
{
    private readonly AppDbContext _db;
    public CourtsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
{
    var courts = await _db.Courts
        .Where(c => c.IsActive)
        .Include(c => c.HappyHourDiscounts)
        .ToListAsync();

    return View(courts);
}
    


    public async Task<IActionResult> Details(int id)
    {
        var court = await _db.Courts
            .Include(c => c.HappyHourDiscounts)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (court == null) return NotFound();
        return View(court);
    }
}
