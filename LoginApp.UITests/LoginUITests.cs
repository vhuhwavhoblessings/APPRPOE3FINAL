using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using System.Threading;

public class LoginUITests
{
    [Fact]
    public void LoginPage_ShouldLoadSuccessfully()
    {
        using (var driver = new ChromeDriver())
        {
            // ✅ Match this to your real URL
            driver.Navigate().GoToUrl("https://localhost:7197/Account/Login");

            // Wait a bit for page to load
            Thread.Sleep(2000);

            // ✅ Check that page title or element is correct
            var title = driver.Title;
            Assert.Contains("Login", title);
        }
    }
}
