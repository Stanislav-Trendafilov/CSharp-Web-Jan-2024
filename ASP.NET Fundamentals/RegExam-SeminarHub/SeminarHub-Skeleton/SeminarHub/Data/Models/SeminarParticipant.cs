using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data.Models
{
	[Comment("This is entity which makes possible many to many relation between classes.")]
	public class SeminarParticipant
	{
		[Comment("Identifier of current seminar.")]
		[Required]
		public int SeminarId { get; set; }

		[ForeignKey(nameof(SeminarId))]
		public Seminar Seminar { get; set; } = null!;

		[Comment("Identifier of the participant who will go on the seminar.")]
		[Required]
		public string ParticipantId { get; set; } = string.Empty;

		[ForeignKey(nameof(ParticipantId))]
		public IdentityUser Participant { get; set; } = null!;


	}


}