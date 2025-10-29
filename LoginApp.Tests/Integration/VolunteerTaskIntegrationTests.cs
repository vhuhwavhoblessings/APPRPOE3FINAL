using Xunit;
using LoginApp.Data;
using LoginApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace LoginApp.Tests.Integration
{
    public class VolunteerTaskIntegrationTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void AddTask_ShouldPersistToDatabase()
        {
            using var context = GetInMemoryDbContext();
            var task = new VolunteerTask
            {
                UserID = 1,
                TaskTitle = "Clean Park",
                Description = "Community clean-up",
                Status = "Pending"
            };

            context.VolunteerTasks.Add(task);
            context.SaveChanges();

            var saved = context.VolunteerTasks.FirstOrDefault(t => t.UserID == 1);
            Assert.NotNull(saved);
            Assert.Equal("Clean Park", saved.TaskTitle);
        }
    }
}
