using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Data.Models
{
	[Comment("Class which holds all information about the seminar.")]
	public class Seminar
	{
		[Key]
		[Comment("This is the id, representing this seminar.")]
		public int Id { get; set; }

		[Required]
		[Comment("This is te topic of the seminar.")]
		[MaxLength(TopicConstants.TopicMaximumLength)]
		public string Topic { get; set; } = string.Empty;

		[Required]
		[Comment("This is the person who will lecture.")]
		[MaxLength(LecturerConstants.LecturerMaximumLength)]
		public string Lecturer { get; set; } = string.Empty;

		[Required]
		[Comment("More details about the event(seminar).")]
		[MaxLength(DetailsConstants.DetailsMaximumLength)]
		public string Details { get; set; } = string.Empty;

		[Required]
		[Comment("Id of the user who is the organizer.")]
		public string OrganizerId { get; set; }=string.Empty;

		[Required]
		[Comment("Organizer as IdentityUser.")]
		[ForeignKey(nameof(OrganizerId))]
		public IdentityUser Organizer { get; set; } = null!;

		[Required]
		[Comment("The date of the seminar.")]
		public DateTime DateAndTime { get; set; }

		[Required]
		[Comment("Duration of the seminar. How long it will take?")]
		[Range(DurationConstants.DurationMinimumLength,DurationConstants.DurationMaximumLength)]
		public int Duration { get; set; }

		[Required]
		[Comment("Id of the seminar category.")]
		public int CategoryId { get; set; }

		[Required]
		[Comment("Category of the seminar.")]
		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; } = null!;

		[Comment("People who will join the seminar (participants).")]
		public IList<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();


	}

}
