using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class DayCostCategory
    {
        public int DayCostCategoryID { get; set; }

        public string DayCostCategoryName { get; set; } = " ";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<DayWiseTourCost> DayWiseTourCosts { get; set; } = new List<DayWiseTourCost>(); 
    }


}
