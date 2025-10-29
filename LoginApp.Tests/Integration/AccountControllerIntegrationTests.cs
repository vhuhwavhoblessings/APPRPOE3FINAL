using Xunit;
using LoginApp.Controllers;
using LoginApp.Data;
using LoginApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace LoginApp.Tests.Integration
{
    public class AccountControllerIntegrationTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Register_ShouldAddUserToDatabase()
        {
            using var context = GetInMemoryDbContext();
            var controller = new AccountController(context);

            var model = new RegisterViewModel
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane@example.com",
                Password = "123456",
                ConfirmPassword = "123456"
            };

            // Call Register method (without relying on session/TempData)
            var result = await controller.Register(model) as RedirectToActionResult;

            // Assert user saved in DB
            var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Email == "jane@example.com");
            Assert.NotNull(savedUser);
            Assert.Equal("Jane", savedUser.FirstName);

            // Assert redirect returned
            Assert.NotNull(result);
            Assert.Equal("Login", result.ActionName);
        }

        [Fact]
        public async Task Login_ShouldReturnRedirect_WhenCredentialsAreValid()
        {
            using var context = GetInMemoryDbContext();
            var controller = new AccountController(context);

            // First, add user manually to DB
            var password = "123456";
            var newUser = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                PasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password))
            };
            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            // Login model
            var loginModel = new UserLogin
            {
                Email = "john@example.com",
                Password = password
            };

            var result = await controller.Login(loginModel) as RedirectToActionResult;

            // Assert redirect returned (we ignore session/TempData)
            Assert.NotNull(result);
        }
    }
}
