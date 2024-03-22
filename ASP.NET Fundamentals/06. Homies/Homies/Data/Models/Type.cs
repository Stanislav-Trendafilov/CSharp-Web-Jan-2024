using System.ComponentModel.DataAnnotations;

namespace Homies.Data.Models
{
	public class Type
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(DataConstants.TypeNameMaximumLength)]
		public string Name { get; set; } = null!;

		public ICollection<Event> Events { get; set; }= new List<Event>();
	}
}
