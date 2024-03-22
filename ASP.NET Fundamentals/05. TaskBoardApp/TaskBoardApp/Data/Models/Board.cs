using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Data.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Board.BoardMaxName)]
        public string Name { get; set; } = null!;

        public IEnumerable<Task> Tasks { get; set; }
            =new List<Task>();
    }
}