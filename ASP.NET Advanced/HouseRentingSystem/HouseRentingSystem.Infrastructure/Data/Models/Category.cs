using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants;

namespace HouseRentingSystem.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } =string.Empty;

        public IEnumerable<House> Houses { get; set; } =new List<House>();
    }


}
