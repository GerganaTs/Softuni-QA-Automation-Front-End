using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System.Text.RegularExpressions;

namespace AppiumCalculatorDesktopApp
{
    public class BaseTests
    {

        private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        // Get-StartApps
        private const string appLocation = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions appiumOptions;

        public WindowsElement windowMaximize => driver.FindElementByAccessibilityId("Maximize");
        public WindowsElement resultField => driver.FindElementByAccessibilityId("CalculatorResults");
        public WindowsElement operationSum => driver.FindElementByAccessibilityId("plusButton");
        public WindowsElement operationMinus => driver.FindElementByAccessibilityId("minusButton");
        public WindowsElement operationMultiply => driver.FindElementByAccessibilityId("multiplyButton");
        public WindowsElement operationDivide => driver.FindElementByAccessibilityId("divideButton");
        public WindowsElement calcButton => driver.FindElementByAccessibilityId("equalButton");
        public WindowsElement clearButton => driver.FindElementByAccessibilityId("clearButton");
        public WindowsElement clearEntryButton => driver.FindElementByAccessibilityId("clearEntryButton]");

        [OneTimeSetUp]
        public void OpenApp()
        {
            this.appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptions.AddAdditionalCapability("app", appLocation);
            this.driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            windowMaximize.Click();
        }

        [OneTimeTearDown]
        public void CloseApp()
        {
            driver.Dispose();
        }

        [Test, Order(1)]
        public void Test_AppName()
        {
            var appName = driver.FindElementByAccessibilityId("AppName");
            Assert.That(appName.Text, Is.EqualTo("Calculator"));
        }

        [TestCase("1","+","1", "2")]
        [TestCase("(-1)", "+", "(-1)", "-2")]
        [TestCase("(-5)", "+", "2", "-3")]
        [TestCase("10", "+", "(-2)", "8")]
        [TestCase("10.10", "+", "22.235", "32.335")]
        [TestCase("(-10.10)", "+", "22.235", "12.135")]
        [TestCase("1", "-", "1", "0")]
        [TestCase("(-1)", "-", "100", "-101")]
        [TestCase("(-50)", "-", "5", "-55")]
        [TestCase("50", "-", "5", "45")]
        [TestCase("10.10", "-", "10.05", "0.05")]
        [TestCase("10", "*", "10", "100")]
        [TestCase("(-10)", "*", "10", "-100")]
        [TestCase("10", "*", "0", "0")]
        [TestCase("10.1", "*", "10.0001", "101.00101")]
        [TestCase("10", "/", "10", "1")]
        [TestCase("(-10)", "/", "10", "-1")]
        [TestCase("10.1", "/", "10.0001", "1.009989900100999")]
        public void Test_CalculateNumbersUsingKeyboard(string firstnum, string operation, string secondnum, string resultExpected)
        {
            clearButton.Click();
            // Arrange
            resultField.SendKeys(firstnum);
            resultField.SendKeys(operation);
            resultField.SendKeys(secondnum);
            //Act
            calcButton.Click();
            //Assert
            var resultActual = Regex.Match(resultField.Text, @"-?[0-9]*[.]?[0-9]+$").Value;
            Assert.That(resultActual, Is.EqualTo(resultExpected));
        }

        [Test]
        public void Test_CalculateNumbersUsingKeyboard_divideByZero() 
        {
            clearButton.Click();
            // Arrange
            resultField.SendKeys("10");
            resultField.SendKeys("/");
            resultField.SendKeys("0");
            //Act
            calcButton.Click();
            //Assert
            var resultActual = resultField.Text;
            Assert.That(resultActual, Is.EqualTo("Display is Cannot divide by zero"));
        }

        [TestCase("5", "!", "5")]
        [TestCase("!", "5", "5")]
        [TestCase("5", "?", "5")]
        [TestCase("?", "5", "5")]
        [TestCase("5", "$", "5")]
        [TestCase("$", "5", "5")]
        [TestCase("5", "&", "5")]
        [TestCase("&", "5", "5")]
        public void Test_CalculateNumbersUsingKeyboard_enterNotAllowedChars(string num1, string notAllowedChar, string result)
        {
            clearButton.Click();
            Thread.Sleep(1000);
            // Arrange
            resultField.SendKeys(num1);
            resultField.SendKeys(notAllowedChar);
            //Act
            calcButton.Click();
            //Assert
            var resultActual = Regex.Match(resultField.Text, @"-?[0-9]*[.]?[0-9]+$").Value;
            Assert.That(resultActual, Is.EqualTo(result));
        }
    }
}

