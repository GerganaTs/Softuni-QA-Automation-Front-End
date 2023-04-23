
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using ExpectedConditions = OpenQA.Selenium.Support.UI.ExpectedConditions;

namespace AppiumDesktopTests
{
    public class AppiumDesktopTests
    {
        private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        // Get-StartApps
        private const string appLocation = "C:\\Users\\Gergana\\Downloads\\Contactbook-Exam-Resources\\ContactBook-DesktopClient.NET6\\ContactBook-DesktopClient.exe";
        private const string myAppLocation = "https://contactbook.gerganatsirkova.repl.co/api";
        private WindowsDriver<WindowsElement> driver;
        private WindowsDriver<WindowsElement> driverNewWindow;
        private AppiumOptions appiumOptions;
        private AppiumOptions appiumOptionsNewWindow;

        [SetUp]
        public void Setup()
        {
            this.appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptions.AddAdditionalCapability("app", appLocation);
            this.driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), appiumOptions);

            this.appiumOptionsNewWindow = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptionsNewWindow.AddAdditionalCapability("app", "Root");
            this.driverNewWindow = new WindowsDriver<WindowsElement>(new Uri(appiumServer), appiumOptionsNewWindow);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void CloseApp()
        {
            driver.Dispose();
        }

        [Test]
        public void Test_Search()
        {
            var apiUrl = driver.FindElementByAccessibilityId("textBoxApiUrl");
            apiUrl.Clear();
            apiUrl.SendKeys(myAppLocation);
            var btnConnect = driver.FindElementByAccessibilityId("buttonConnect");
            btnConnect.Click();
            
            var searchInputField = driverNewWindow.FindElementByAccessibilityId("textBoxSearch");
            searchInputField.SendKeys("steve");
            var btnSearch = driverNewWindow.FindElementByAccessibilityId("buttonSearch");
            btnSearch.Click();
            Thread.Sleep(2000);
            var firstName = driverNewWindow.FindElementByName("FirstName Row 0, Not sorted.");
            Assert.That(firstName.Text, Is.EqualTo("Steve"));
            var lastName = driverNewWindow.FindElementByName("LastName Row 0, Not sorted.");
            Assert.That(lastName.Text, Is.EqualTo("Jobs"));
        }

    }
}