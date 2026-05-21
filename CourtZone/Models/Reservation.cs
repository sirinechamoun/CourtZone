using System.ComponentModel.DataAnnotations;

namespace CourtZone.Models;

public class Reservation
{
    public int Id { get; set; }

    [Required]
    public int CourtId { get; set; }
    public Court? Court { get; set; }

    [Required, StringLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required]
    public DateTime StartTime { get; set; }

    [Range(1, 5)]
    public double DurationHours { get; set; } = 1;

    public decimal OriginalPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal FinalPrice { get; set; }

    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime EndTime => StartTime.AddHours(DurationHours);
}
