using Xunit;
using LoginApp.Data;
using LoginApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace LoginApp.Tests.Integration
{
    public class ResourceDonationIntegrationTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void AddDonation_ShouldPersistToDatabase()
        {
            using var context = GetInMemoryDbContext();
            var donation = new ResourceDonation
            {
                UserID = 1,
                ResourceType = "Food",
                Quantity = 10,
                Location = "Community Center"
            };

            context.ResourceDonations.Add(donation);
            context.SaveChanges();

            var saved = context.ResourceDonations.FirstOrDefault(d => d.UserID == 1);
            Assert.NotNull(saved);
            Assert.Equal("Food", saved.ResourceType);
        }
    }
}
