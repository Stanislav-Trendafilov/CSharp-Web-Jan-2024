using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data.Models;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Data
{
    public class TaskBoardAppDbContext:IdentityDbContext<IdentityUse>
    {
        private IdentityUser TestUser { get; set; }
        private Board OpenBoard { get; set; }
        private Board InProgressBoard { get; set; }
        private Board DoneBoard { get; set; }
        public TaskBoardAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Board> Boards { get; set; }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Task>()
                .HasOne(a => a.Board)
                .WithMany(a => a.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedUsers();
            builder
                .Entity<IdentityUser>()
                    .HasData(TestUser);

            SeedBoards();
            builder
                .Entity<Board>()
                    .HasData(OpenBoard, InProgressBoard, DoneBoard);

            SeedBoards();
            builder
                .Entity<Task>()
                .HasData(new Task()
                {
                    Id = 1,
                    Title = "Improve CSS",
                    Description = "Implement comments for all pages",
                    CreatedOn = DateTime.Now.AddDays(-5),
                    OwnerId = TestUser.Id,
                    BoardId = OpenBoard.Id
                },
                new Task()
                {
                    Id = 2,
                    Title = "Improve Code Quality",
                    Description = "Implement styles for all pages",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = TestUser.Id,
                    BoardId = OpenBoard.Id
                },
                new Task()
                {
                    Id = 3,
                    Title = "Improve Html Quality",
                    Description = "Implement divs for all pages",
                    CreatedOn = DateTime.Now.AddDays(-1),
                    OwnerId = TestUser.Id,
                    BoardId = InProgressBoard.Id
                },
                new Task()
                {
                    Id = 4,
                    Title = "Write Documentation",
                    Description = "Take screenshots",
                    CreatedOn = DateTime.Now.AddDays(-1),
                    OwnerId = TestUser.Id,
                    BoardId = DoneBoard.Id
                })
                ;


            base.OnModelCreating(builder);
        }

        private void SeedBoards()
        {
            OpenBoard = new Board()
            {
                Id = 1,
                Name = "Open"
            };

            InProgressBoard = new Board()
            {
                Id = 2,
                Name = "In Progress"
            };

            DoneBoard = new Board()
            {
                Id = 3,
                Name = "Done"
            };
        }
        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            TestUser = new IdentityUser()
            {

                UserName = "sslav2007@gmail.com",
                NormalizedUserName = "SSLAV2007@GMAIL.COM"
            };
            TestUser.PasswordHash = hasher.HashPassword(TestUser, "123456");
        }
    }
}
