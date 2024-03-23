using HouseRentingSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Infrastructure.Data.Common
{
    public class Repository : IRepository
    {
        private readonly DbContext context;
        public Repository(HouseRentingDbContext _context)
        {
            context = _context;
        }
        public IQueryable<T> All<T>() where T : class
            => DbSet<T>();
        public IQueryable<T> AllReadOnly<T>() where T : class
            => DbSet<T>().AsNoTracking();
        private DbSet<T> DbSet<T>() where T : class
            => context.Set<T>();


        public async Task AddAsync<T>(T entity) where T : class
        {
            await DbSet<T>().AddAsync(entity);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
        public async Task<T?> GetByIdAsync<T>(int id) where T : class
            => await DbSet<T>().FindAsync(id);
    }
}
