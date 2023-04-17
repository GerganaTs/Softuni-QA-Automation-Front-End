using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System.Collections.Generic;

namespace AppiumCalculatorTests
{
    public class CalculatorTests
    {
        private const string appiumServer = "http://127.0.1:4723/wd/hub";
        private const string appLocation = @"C:\Apps\softuni\SummatorDesktopApp.exe";
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions appiumOptions;
        // private AppiumLocalService appiumLocalService;

        [OneTimeSetUp]
        public void OpenApplication()
        {
            // Start using Desktop Appium SErver
            this.appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", appLocation);
            appiumOptions.AddAdditionalCapability("PlatformName", "Windows");
            this.driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            // Start using Appium Headless mode; This way of work faster 
            /*this.appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            appiumLocalService.Start();
            this.appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", appLocation);
            appiumOptions.AddAdditionalCapability("PlatformName", "Windows");
            this.driver = new WindowsDriver<WindowsElement>(appiumLocalService, appiumOptions);*/
        }

        [OneTimeTearDown]
        public void CloseApplication()
        {
            driver.CloseApp();
            driver.Quit();
        }

        [TestCase("5","5", "10")]
        [TestCase("20","5", "25")]
        [TestCase("5","", "error")]
        [TestCase("", "5", "error")]
        [TestCase("", "", "error")]
        public void Test_Sum_Numbers(string first, string second, string result)
        {
            var firstField = driver.FindElementByAccessibilityId("textBoxFirstNum");
            var secondField = driver.FindElementByAccessibilityId("textBoxSecondNum");
            var resultField = driver.FindElementByAccessibilityId("textBoxSum");
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            firstField.Clear();
            secondField.Clear();
            firstField.SendKeys(first);
            secondField.SendKeys(second);
            calcButton.Click();

            Assert.That(resultField.Text, Is.EqualTo(result));
        }
    }
}