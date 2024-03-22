using System.ComponentModel.DataAnnotations;

namespace ProductShop.Data.Models
{
    public class Product
    {
        public Product()
        {
            ProductNotes = new List<ProductNote>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<ProductNote> ProductNotes { get; set; }

    }
}