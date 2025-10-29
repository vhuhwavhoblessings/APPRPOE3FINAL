using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LoginApp.Models;
using Xunit;

namespace LoginApp.Tests.Models
{
    /// <summary>
    /// Tests for User model
    /// </summary>
    public class UserTests
    {
        [Fact]
        public void CreatedDate_ShouldHaveDefaultValue()
        {
            var user = new User();
            Assert.True(user.CreatedDate <= DateTime.Now);
        }

        [Fact]
        public void IsActive_ShouldBeTrueByDefault()
        {
            var user = new User();
            Assert.True(user.IsActive);
        }

        [Fact]
        public void Model_ShouldFailValidation_WhenRequiredFieldsMissing()
        {
            var user = new User();
            var results = ValidateModel(user);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void Model_ShouldFail_WhenPasswordsDoNotMatch()
        {
            var user = new User
            {
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice@example.com",
                Password = "123456",
                ConfirmPassword = "654321"
            };

            var results = ValidateModel(user);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Passwords do not match"));
        }

        [Fact]
        public void Model_ShouldPassValidation_WhenAllFieldsAreValid()
        {
            var user = new User
            {
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice@example.com",
                Password = "123456",
                ConfirmPassword = "123456"
            };

            var results = ValidateModel(user);
            Assert.Empty(results);
        }

        // Helper to validate attributes
        private IList<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
    }
}
