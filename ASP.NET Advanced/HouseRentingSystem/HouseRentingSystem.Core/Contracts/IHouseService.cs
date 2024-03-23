using HouseRentingSystem.Core.Models.Houses;
using HouseRentingSystem.Models.Houses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Services
{
	public interface IHouseService
	{
		Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync();
		Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync();

		Task<bool> CategoryExistsAsync(int categoryId);

		Task<int> CreateAsync(HouseFormModel model,int agentId);

	}

}
