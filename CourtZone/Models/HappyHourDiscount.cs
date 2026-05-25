using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtZone.Models;
// for discounts
public class HappyHourDiscount
{
    public int Id { get; set; }

    [Required]
    public int CourtId { get; set; }//view models
    public Court? Court { get; set; }

    [Required]
    public DayOfWeek DayOfWeek { get; set; }

    // Oracle-friendly storage: minutes after midnight.
    // Example: 10:00 AM = 600, 2:00 PM = 840.
    [Range(0, 1439)]
    [Display(Name = "Start Time")]
    public int StartMinute { get; set; }

    [Range(1, 1440)]
    [Display(Name = "End Time")]
    public int EndMinute { get; set; }

    [Range(1, 90)]
    public int DiscountPercentage { get; set; }

    public bool IsActive { get; set; } = true;

    [NotMapped]
    [Display(Name = "Start Time")]
    public string StartTimeText
    {
        get => TimeSpan.FromMinutes(StartMinute).ToString(@"hh\:mm");
        set => StartMinute = ParseTimeToMinutes(value);
    }

    [NotMapped]
    [Display(Name = "End Time")]
    public string EndTimeText
    {
        get => TimeSpan.FromMinutes(EndMinute).ToString(@"hh\:mm");
        set => EndMinute = ParseTimeToMinutes(value);
    }

    [NotMapped]
    public string TimeRangeText => $"{StartTimeText} - {EndTimeText}";

    private static int ParseTimeToMinutes(string? value)
    {
        if (TimeSpan.TryParse(value, out var time))
        {
            return (int)time.TotalMinutes;
        }

        return 0;
    }
}
