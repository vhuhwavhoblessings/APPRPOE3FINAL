using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LoginApp.Models;
using Xunit;

namespace LoginApp.Tests
{
    public class IncidentReportTests
    {
        [Fact]
        public void DateReported_ShouldHaveDefaultValue()
        {
            var report = new IncidentReport();
            Assert.True(report.DateReported <= DateTime.Now);
        }

        [Fact]
        public void Model_ShouldFailValidation_WhenRequiredFieldsMissing()
        {
            var report = new IncidentReport();
            var results = ValidateModel(report);
            Assert.True(results.Count > 0);
        }

        [Fact]
        public void Model_ShouldPassValidation_WhenAllRequiredFieldsPresent()
        {
            var report = new IncidentReport
            {
                UserID = 1,
                Title = "System Crash",
                Description = "System crashed after login attempt",
                Location = "Main Office"
            };

            var results = ValidateModel(report);
            Assert.Empty(results);
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
    }
}
