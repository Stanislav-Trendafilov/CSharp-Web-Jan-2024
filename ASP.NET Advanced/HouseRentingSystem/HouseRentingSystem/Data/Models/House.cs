using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Data.DataConstants;

namespace HouseRentingSystem.Data.Models
{
    public class House
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(HouseTitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(HouseAddressMaxLength)]
        public string Address  { get; set; } = string.Empty;

        [Required]
        [MaxLength(HouseDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Range(0.0,2000.0)]
        public decimal PricePerMonth { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        public Category Agent { get; set; } = null!;

        public string? RenterId{ get; set; } 

    }         

}
