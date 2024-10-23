using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DTOs.InputModels
{
    public class ScheduleInsertModel
    {
        public int TourVoucherID { get; set; }
        public string ScheduleTitle { get; set; } // "JourneyStart/LunchBreak/MeetingTiming";
        public string? ScheduleDescription { get; set; }
       // public int PackageID { get; set; } 
        public int DayNumber { get; set; } 
        public DateTime TentativeTime { get; set; }
        public DateTime? ActualTime { get; set; }

        public decimal TentativeCost { get; set; }
        public decimal? ActualCost { get; set; }

        public int DayCostCategoryID { get; set; }
    }


}
