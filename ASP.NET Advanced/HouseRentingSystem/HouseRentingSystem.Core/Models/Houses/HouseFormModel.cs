﻿using HouseRentingSystem.Core.Models.Houses;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Core.Constants.MessageConstants;
using static HouseRentingSystem.Data.DataConstants;
namespace HouseRentingSystem.Models.Houses
{
	public class HouseFormModel
	{
		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(HouseTitleMaxLength,
			MinimumLength = HouseTitleMinLength,
			ErrorMessage = LengthMessage)]
		public string Title { get; set; } = null!;

		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(HouseAddressMaxLength,
			MinimumLength = HouseTitleMinLength,
			ErrorMessage =LengthMessage)]
		public string Address { get; set; }=null!;

		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(HouseDescriptionMaxLength,
			MinimumLength = HouseTitleMinLength,
			ErrorMessage = LengthMessage)]
		public string Description { get; set; } = null!;

		[Required(ErrorMessage = RequiredMessage)]
		[Display(Name ="Image URL")]
		public string ImageUrl { get; set; } = null!;

		[Required(ErrorMessage = RequiredMessage)]
		[Range(typeof(decimal),"0.00",HouseRentingPriceMaximum,ErrorMessage ="Price per month must be a positive number and less than {2} leva")]
		[Display(Name ="Price Per Month")]
		public decimal PricePerMonth { get; set; }

		[Display(Name ="Category")]
		public int CategoryId { get; set; }

		public IEnumerable<HouseCategoryServiceModel> Categories { get; set; }		=new List<HouseCategoryServiceModel>();

	}
}
