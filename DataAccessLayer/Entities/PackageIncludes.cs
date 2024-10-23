using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class PackageIncludes
    {
        [Key]
        public int IncludeID { get; set; }

        [Required]
        public int PackageID { get; set; }

        [Required]
        [StringLength(1000)]  
        public string IncludeDescription { get; set; } 

        [ForeignKey("PackageID")]
        public Package Package { get; set; }
    }

}
