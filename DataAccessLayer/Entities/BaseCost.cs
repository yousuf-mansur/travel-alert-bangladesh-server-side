using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class BaseCost
    {
        public int BaseCostID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public Decimal TentativeCost { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public Decimal ActualCost { get; set; }
        
    }
}
