using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DTOs.InputModels
{
    public class PackageBudgetInsertModel
    {
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
    }

    public class PackageBudgetInsertModelPart
    {
       

        [Column(TypeName = "decimal(18,2)")]
        public decimal OtherCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProfitPercent { get; set; }
    }

}
