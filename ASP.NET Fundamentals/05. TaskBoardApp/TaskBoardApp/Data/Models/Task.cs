using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoardApp.Data.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Task.TaskMaxTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.Task.TaskMaxDescription)]
        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int BoardId { get; set; }

        [ForeignKey(nameof(BoardId))]
        public Board? Board { get; set; }

        public string OwnerId { get; set; } = null!;

		[ForeignKey(nameof(OwnerId))]
		public IdentityUser Owner { get; set; } = null!;

	}
}
