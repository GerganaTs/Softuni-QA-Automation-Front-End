global using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using AppiumCalculatorPOM.Pages;


namespace AppiumCalculatorPOM.Tests
{
    public class BaseTests
    {
        public const string appiumServer = "http://127.0.1:4723/wd/hub";
        public const string appLocation = @"C:\Apps\softuni\SummatorDesktopApp.exe";
        public WindowsDriver<WindowsElement> driver;
        public AppiumOptions appiumOptions;

        [OneTimeSetUp]
        public void Setup()
        {
            this.appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", appLocation);
            appiumOptions.AddAdditionalCapability("PlatformName", "Windows");
            this.driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        [OneTimeTearDown]
        public void CloseApplication()
        {
            driver.CloseApp();
            driver.Quit();
        }
    }
}
