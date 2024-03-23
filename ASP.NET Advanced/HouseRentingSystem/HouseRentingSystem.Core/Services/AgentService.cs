using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Data.Models;
using HouseRentingSystem.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository repository;

        public AgentService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateAsync(string userId, string phoneNumber)
        {
            await repository.AddAsync(new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            });

            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistByIdAsync(string userId)
        {
            return await repository.AllReadOnly<Agent>()
                .AnyAsync(a => a.UserId == userId);
        }

        public async Task<int?> GetAgentIdAsync(string userId)
        {           
            return  (await repository.AllReadOnly<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId))?.Id;
        }

        public async Task<bool> UserHasRentsAsync(string userId)
        {
            return await repository.AllReadOnly<House>()
                 .AnyAsync(h => h.RenterId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
        {
            return await repository.AllReadOnly<Agent>()
                 .AnyAsync(h => h.PhoneNumber == phoneNumber);
        }
    }
}
