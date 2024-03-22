using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.DataConstants;
namespace SeminarHub.Models
{
	public class SeminarFormViewModel
	{
		[Required(ErrorMessage = RequireErrorMessage)]
		[StringLength(TopicConstants.TopicMaximumLength,
			MinimumLength = TopicConstants.TopicMinimumLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Topic { get; set; }=string.Empty;

		[Required(ErrorMessage = RequireErrorMessage)]
		[StringLength(LecturerConstants.LecturerMaximumLength,
			MinimumLength = LecturerConstants.LecturerMinimumLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Lecturer { get; set; } = string.Empty;

		[Required(ErrorMessage = RequireErrorMessage)]
		[StringLength(DetailsConstants.DetailsMaximumLength,
			MinimumLength = LecturerConstants.LecturerMinimumLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Details { get; set; } = string.Empty;

		[Required(ErrorMessage = RequireErrorMessage)]
		[Range(DurationConstants.DurationMinimumLength,DurationConstants.DurationMaximumLength)]
		public int Duration { get; set; }

		[Required(ErrorMessage = RequireErrorMessage)]
		public string DateAndTime { get; set; } = string.Empty;

		[Required(ErrorMessage = RequireErrorMessage)]
		public int CategoryId { get; set; }

		public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

	}
}
