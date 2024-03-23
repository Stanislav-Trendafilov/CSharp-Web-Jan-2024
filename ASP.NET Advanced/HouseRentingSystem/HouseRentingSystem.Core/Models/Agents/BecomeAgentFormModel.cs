using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Core.Constants.MessageConstants;
using static HouseRentingSystem.Data.DataConstants;
namespace HouseRentingSystem.Models.Agents
{
	public class BecomeAgentFormModel
	{
		[Required(ErrorMessage =RequiredMessage)]
		[StringLength(AgentPhoneNumberMaxLength,MinimumLength =AgentPhoneNumberMinLength,ErrorMessage =LengthMessage)]
		[Display(Name ="Phone number")]
		[Phone]
		public string PhoneNumber { get; set; } = null!;
	}
}
