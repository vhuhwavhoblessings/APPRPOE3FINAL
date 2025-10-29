using Xunit;
using LoginApp.Data;
using LoginApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace LoginApp.Tests.Integration
{
    public class UserIntegrationTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void AddUser_ShouldSaveToDatabase()
        {
            using var context = GetInMemoryDbContext();
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                PasswordHash = "hashed",
                ConfirmPassword = "hashed"
            };

            context.Users.Add(user);
            context.SaveChanges();

            var savedUser = context.Users.FirstOrDefault(u => u.Email == "john@example.com");
            Assert.NotNull(savedUser);
            Assert.Equal("John", savedUser.FirstName);
        }
    }
}
