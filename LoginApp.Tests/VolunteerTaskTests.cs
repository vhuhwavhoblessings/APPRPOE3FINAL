using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LoginApp.Models;
using Xunit;

namespace LoginApp.Tests.Models
{
    /// <summary>
    /// Tests for VolunteerTask model
    /// </summary>
    public class VolunteerTaskTests
    {
        [Fact]
        public void TaskDate_ShouldHaveDefaultValue()
        {
            var task = new VolunteerTask();
            Assert.True(task.TaskDate <= DateTime.Now);
        }

        [Fact]
        public void Status_ShouldBePendingByDefault()
        {
            var task = new VolunteerTask();
            Assert.Equal("Pending", task.Status);
        }

        [Fact]
        public void Model_ShouldFailValidation_WhenRequiredFieldsMissing()
        {
            var task = new VolunteerTask();
            var results = ValidateModel(task);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void Model_ShouldPassValidation_WhenAllFieldsAreValid()
        {
            var task = new VolunteerTask
            {
                UserID = 1,
                TaskTitle = "Clean Up Park",
                Description = "Organize a community park clean-up event",
                Status = "Pending"
            };

            var results = ValidateModel(task);
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
