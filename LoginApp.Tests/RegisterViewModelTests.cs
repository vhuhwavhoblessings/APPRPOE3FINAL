using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LoginApp.Models;
using Xunit;

namespace LoginApp.Tests.Models
{
    /// <summary>
    /// Tests for RegisterViewModel class
    /// </summary>
    public class RegisterViewModelTests
    {
        [Fact]
        public void Model_ShouldFailValidation_WhenRequiredFieldsMissing()
        {
            // Arrange: create an empty model (missing required fields)
            var model = new RegisterViewModel();

            // Act: validate the model
            var results = ValidateModel(model);

            // Assert: should have validation errors
            Assert.NotEmpty(results);
        }

        [Fact]
        public void Model_ShouldFail_WhenPasswordsDoNotMatch()
        {
            var model = new RegisterViewModel
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane@example.com",
                Password = "123456",
                ConfirmPassword = "654321" // does not match
            };

            var results = ValidateModel(model);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Passwords do not match"));
        }

        [Fact]
        public void Model_ShouldPassValidation_WhenAllFieldsAreValid()
        {
            var model = new RegisterViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Password = "123456",
                ConfirmPassword = "123456"
            };

            var results = ValidateModel(model);
            Assert.Empty(results);
        }

        // Helper method to validate attributes
        private IList<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
    }
}
