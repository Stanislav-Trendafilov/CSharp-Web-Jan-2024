using HouseRentingSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseRentingSystem.Data
{
    public class HouseRentingDbContext : IdentityDbContext
    {
        public HouseRentingDbContext(DbContextOptions<HouseRentingDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Agent> Agents { get; set; } = null!;
        public DbSet<House> Houses { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AgentConfiguration());
            builder.ApplyConfiguration(new HouseConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(builder);
        }
    }

    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            var data = new SeedData();
            builder.HasData(new Agent[] { data.Agent });
        }
    }
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            var data = new SeedData();
            builder.HasData(new Category[] { data.CottageCategory, data.SingleCategory, data.DuplexCategory });
        }
    }
    public class HouseConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            //builder
            //    .HasOne(h => h.Category)
            //    .WithMany(c => c.Houses)
            //    .HasForeignKey(h => h.CategoryId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .HasOne(h => h.Agent)
            //    .WithMany(a => a.Houses)
            //    .HasForeignKey(h => h.AgentId)
            //    .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(h => h.Category)
                .WithMany(c => c.Houses)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(h => h.Agent)
                .WithMany()
                .HasForeignKey(h => h.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            var data = new SeedData();
            builder.HasData(new House[] { data.FirstHouse, data.SecondHouse, data.ThirdHouse });
        }
    }
    public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            var data = new SeedData();
            builder.HasData(new IdentityUser[] { data.AgentUser, data.GuestUser });
        }
    }
}