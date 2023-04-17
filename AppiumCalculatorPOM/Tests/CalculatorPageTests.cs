global using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using AppiumCalculatorPOM.Pages;

namespace AppiumCalculatorPOM.Tests
{
    public class CalculatorPageTests : BaseTests
    {

        private CalculatorPage page;

        [OneTimeSetUp]
        public void Setup()
        {
            this.page = new CalculatorPage(driver);
        }

        [OneTimeTearDown]
        public void CloseApplication()
        {
            driver.CloseApp();
            driver.Quit();
        }

        [TestCase("5", "5", "10")]
        [TestCase("20", "5", "25")]
        [TestCase("5", "", "error")]
        [TestCase("", "5", "error")]
        [TestCase("", "", "error")]
        public void Test_Sum_Numbers(string fieldOne, string fieldTwo, string finalResult)
        {
            var resultTest = page.CaclulateNumbers(fieldOne, fieldTwo);
            Assert.That(resultTest, Is.EqualTo(finalResult));
        }
    }
}