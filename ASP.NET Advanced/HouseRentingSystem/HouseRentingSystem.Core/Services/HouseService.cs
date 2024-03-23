using HouseRentingSystem.Core.Models.Houses;
using HouseRentingSystem.Core.Services;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using HouseRentingSystem.Models.Houses;

namespace HouseRentingSystem.Core.Contracts
{
    public class HouseService : IHouseService
	{
		private readonly IRepository repository;

		public HouseService(IRepository _repository)
		{
			   repository= _repository;
		}

        public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync()
        {
           return await repository.AllReadOnly<Category>()
				.Select(c => new HouseCategoryServiceModel
				{
					Id=c.Id,
					Name=c.Name,
				})
				.ToListAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
			return await repository.AllReadOnly<Category>()
				   .AnyAsync(x => x.Id == categoryId);
        }

        public async Task<int> CreateAsync(HouseFormModel model, int agentId)
        {
            House house = new House()
            {
                Address = model.Address,
                AgentId=agentId,
				CategoryId=model.CategoryId,
				Description=model.Description,
				ImageUrl=model.ImageUrl,
				Title=model.Title,
				PricePerMonth=model.PricePerMonth
            };

			await repository.AddAsync(house);
			await repository.SaveChangesAsync();

			return house.Id;
        }

        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync()
		{
			return await repository
				.AllReadOnly<Data.Models.House>()
				.OrderByDescending(h=>h.Id)
				.Take(3)
				.Select(h=>new HouseIndexServiceModel()
				{
					 Id=h.Id,
					 ImageUrl=h.ImageUrl,
					 Title=h.Title
					 
				}).ToListAsync();
		}
	}
}
