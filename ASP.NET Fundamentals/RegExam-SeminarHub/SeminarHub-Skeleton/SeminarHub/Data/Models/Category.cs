using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Data.Models
{
	[Comment("This class contains information about category of the seminar.")]
	public class Category
	{
		[Key]
		[Comment("Identifier of the seminar category.")]
		public int Id { get; set; }

		[Required]
		[Comment("Name of the category.")]
		[MaxLength(CategoryConstants.CategoryNameMaximumLength)]
		public string Name { get; set; } = string.Empty;

		[Comment("This collection holds all the seminars which are in this category.")]
		public IList<Seminar> Seminars { get; set; } = new List<Seminar>();
	}
}