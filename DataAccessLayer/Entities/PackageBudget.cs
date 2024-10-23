using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class PackageBudget
    {
        // calculated values
        public int PackageBudgetID { get; set; }
        public int PackageID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal EstimateedFoodCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal EstimatedTransportCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal EstimatedAccomodationCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OtherCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProfitPercent { get; set; }

        [NotMapped]
        public decimal IndividualPackageCost { get; set; }
        [NotMapped]
        public decimal TotalPackageCost { get; set; }

        public virtual Package? Package { get; set; }

    }
}
