using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class FoodItem /*: BaseClass*/
    {
        [Key]
        public int FoodItemID { get; set; }  
        [Required]
        public string ItemName { get; set; }      
        [Required]
        public DateTime CreatedAt { get; set; }=DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; }= DateTime.Now;

        public ICollection<PackageFoodItem> PackageMenus { get; set; }= new List<PackageFoodItem>();
    }

}



  