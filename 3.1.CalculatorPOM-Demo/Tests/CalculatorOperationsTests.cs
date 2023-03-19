using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using _3._1.CalculatorPOM.Pages;
using System.Collections.Generic;

namespace _3._1.CalculatorPOM.Tests
{
    public class CalculatorOperationsPageTests : BaseTests
    {
        private CalculatorOperationsPage page;
        
        [SetUp]
        public void Setup()
        {
            this.page = new CalculatorOperationsPage(driver);
            page.Open();
        }

        [Test]
        public void CalculatorOperationsPage_CheckTitle()
        {
            Assert.That(page.GetPageTitle(), Is.EqualTo("Number Calculator"));
        }

        [TestCase("", "-", "3", "Result: invalid input")]
        [TestCase("", "*", "3", "Result: invalid input")]
        [TestCase("", "/", "3", "Result: invalid input")]
        [TestCase("", "/+", "3", "Result: invalid input")]
        [TestCase("12", "/", "3", "Result: 4")]
        [TestCase("12.5", "/", "4", "Result: 3.125")]
        [TestCase("32", "-", "infinity", "Result: invalid input")]
        [TestCase("3", "!!!!!!!", "7", "Result: invalid operation")]

        public void CalculatorOperationsPage_Operation(string first, string operation, string second, string result) {
            var actualResult = page.CalculateNumbers(first, operation, second);
            Assert.That(actualResult, Is.EqualTo(result));
        }

        [Test]
        public void CalculatorOperationsPage_TestResetForm()
        {
            page.CalculateNumbers("5", "+", "5");
            page.ResetButton.Click();
            Assert.True(page.IsFormReset());
        }
    }
}