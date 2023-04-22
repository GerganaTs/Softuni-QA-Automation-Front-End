global using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using ExpectedConditions = OpenQA.Selenium.Support.UI.ExpectedConditions;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;
using System.Linq;

namespace AppiumDesktopTests
{
    public class AppiumDesktopTests
    {
        private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        // Get-StartApps
        private const string appLocation = "C:\\Users\\Gergana\\Downloads\\ExamPrepResources\\ShortURL-DesktopClient-v1.0.net6\\ShortURL-DesktopClient.exe";
        private const string myAppLocation = "https://shorturl-3.gerganatsirkova.repl.co/api";
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions appiumOptions;

        [OneTimeSetUp]
        public void Setup()
        {
            this.appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptions.AddAdditionalCapability("app", appLocation);
            this.driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var btnConnect = driver.FindElementByAccessibilityId("buttonConnect");
            btnConnect.Click();
        }

        [OneTimeTearDown]
        public void CloseApp()
        {
            driver.Dispose();
        }

        [Test]
        public void AddNewUrl()
        {
            var url = "https://dnes.bg";
            var btnAdd = driver.FindElementByAccessibilityId("buttonAdd");
            btnAdd.Click();
            var fieldUrl = driver.FindElementByAccessibilityId("textBoxURL");
            fieldUrl.SendKeys(url);
            var btnCreate = driver.FindElementByAccessibilityId("buttonCreate");
            btnCreate.Click();
            var btnReload = driver.FindElementByAccessibilityId("buttonReload");
            btnReload.Click();
            var html = driver.PageSource;
            var newUrl = driver.FindElementByXPath(@"//Text[@Name ='https://wikipedia.org']");
            Assert.True(html.Contains(url));
            Assert.That(html.Contains(newUrl.Text), Is.True);
        }

    }
}