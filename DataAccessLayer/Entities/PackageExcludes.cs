using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class PackageExcludes
    {
        [Key]
        public int ExcludeID { get; set; }

        [Required]
        public int PackageID { get; set; }

        [Required]
        [StringLength(1000)]  
        public string ExcludeDescription { get; set; } 

        // Navigation properties
        [ForeignKey("PackageID")]
        public Package Package { get; set; }
    }
}
