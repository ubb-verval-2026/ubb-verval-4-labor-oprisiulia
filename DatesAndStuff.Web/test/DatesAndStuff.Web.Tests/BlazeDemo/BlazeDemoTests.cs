using NUnit.Framework;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DatesAndStuff.Web.Tests;

[TestFixture]
public class BlazeDemoTests
    {
        private IWebDriver driver;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void TeardownTest()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void BlazeDemo_MexicoCity_To_Dublin_ShouldHaveAtLeastThreeFlights()
        {
            driver.Navigate().GoToUrl("https://blazedemo.com");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var departure = new SelectElement(driver.FindElement(By.Name("fromPort")));
            departure.SelectByText("Mexico City");

            var destination = new SelectElement(driver.FindElement(By.Name("toPort")));
            destination.SelectByText("Dublin");

            driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            var flights = wait.Until(d => {
                var rows = d.FindElements(By.CssSelector("table.table tbody tr"));
                return rows.Count >= 3 ? rows : null;
            });

            flights.Count.Should().BeGreaterThanOrEqualTo(3);
        }
    }
