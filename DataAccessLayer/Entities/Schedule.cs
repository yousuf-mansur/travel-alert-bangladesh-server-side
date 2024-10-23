using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int TourVoucherID { get; set; }
        public string ScheduleTitle { get; set; } = "JourneyStart/LunchBreak/MeetingTiming";
        public string? ScheduleDescription { get; set; }
        public int PackageID { get; set; } = 1;
        public Package? Package { get; set; }
        public int DayNumber { get; set; } = 1;
        public DateTime TentativeTime { get; set; }
        public DateTime? ActualTime { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TentativeCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ActualCost { get; set; }

        // Foreign key for DayCostCategory
        public int DayCostCategoryID { get; set; }
        public DayCostCategory DayCostCategory { get; set; }  // Navigation property

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime DeletedAt { get; set; } = DateTime.Now;

        public virtual TourVoucher? TourVoucher { get; set; }
    }



}
