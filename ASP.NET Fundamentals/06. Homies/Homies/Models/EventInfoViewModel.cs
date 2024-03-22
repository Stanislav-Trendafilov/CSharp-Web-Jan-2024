using Homies.Data;

namespace Homies.Models
{
	public class EventInfoViewModel
	{
		public EventInfoViewModel(int id, string name, DateTime startingTime, string type, string organiser)
		{
			Id = id;
			Name = name;
			Start = startingTime.ToString(DataConstants.DateFormat);
			Type = type;
			Organiser = organiser;
		}

		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string Start { get; set; } = null!;

		public string Type { get; set; } = null!;

		public string Organiser { get; set; } = null!;
	}
}
