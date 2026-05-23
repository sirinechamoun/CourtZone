using CourtZone.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace CourtZone.Controllers;
[Authorize]
public class HomeController : Controller
// system architecure
{
    private readonly AppDbContext _db;
    public HomeController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
    //implement advannce
        var courts = await _db.Courts.Where(c => c.IsActive).ToListAsync();
        return View(courts);
    }
}
