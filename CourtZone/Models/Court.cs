using System.ComponentModel.DataAnnotations;

namespace CourtZone.Models;

public class Court
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public SportType Sport { get; set; }

    [Range(0, 1000)]
    public decimal PricePerHour { get; set; }

    [StringLength(250)]
    public string? Location { get; set; }

    // Lebanese location info
    public string Area { get; set; } = "";

    public string City { get; set; } = "";

    public double DistanceFromBeirutKm { get; set; }

    // Stadium image URL
    [Display(Name = "Image URL")]
    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public List<Reservation> Reservations { get; set; } = new();

    public List<HappyHourDiscount> HappyHourDiscounts { get; set; } = new();
}
