using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.InputModels
{
    public class PackageExcludesInsertModel
    {
        [Required]
        public int PackageID { get; set; }

        [Required]
        [StringLength(1000)]
        public string ExcludeDescription { get; set; } = " ";
    }

}
