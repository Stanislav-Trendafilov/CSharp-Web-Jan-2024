namespace SeminarHub.Data
{
	public static class DataConstants
	{
		public class TopicConstants
		{
			public const int TopicMinimumLength = 3;
			public const int TopicMaximumLength = 100;
		}
		public class LecturerConstants
		{
			public const int LecturerMinimumLength = 5;
			public const int LecturerMaximumLength = 60;
		}
		public class DetailsConstants
		{
			public const int DetailsMinimumLength =10;
			public const int DetailsMaximumLength = 500;
		}
		public class DurationConstants
		{
			public const int DurationMinimumLength = 30;
			public const int DurationMaximumLength = 180;
		}

		public const string DateFormat = "dd/MM/yyyy HH:mm";

		public class CategoryConstants
		{
			public const int CategoryNameMinimumLength = 3;
			public const int CategoryNameMaximumLength = 50;
		}

		public const string RequireErrorMessage = "The field {0} is required";
		public const string StringLengthErrorMessage = "The field {0} must be between {2} and {1} characters long";

	}
}
