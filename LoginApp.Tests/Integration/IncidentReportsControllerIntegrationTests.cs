using Xunit;
using LoginApp.Controllers;
using LoginApp.Data;
using LoginApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace LoginApp.Tests.Integration
{
    public class IncidentReportsControllerIntegrationTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Create_ShouldSaveIncidentReport()
        {
            using var context = GetInMemoryDbContext();
            var user = new User { FirstName = "John", LastName = "Doe", Email = "john@example.com", PasswordHash = "hash" };
            context.Users.Add(user);
            context.SaveChanges();

            var controller = new IncidentReportsController(context);
            var model = new IncidentReport
            {
                Title = "System Crash",
                Description = "App crashed on login",
                Location = "Main Office",
                UserID = user.UserID
            };

            var result = await controller.Create(model) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Single(context.IncidentReports.ToList());
        }
    }
}
