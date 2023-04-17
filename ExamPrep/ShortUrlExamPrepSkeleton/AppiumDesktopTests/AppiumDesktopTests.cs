using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using System.Linq;

namespace AppiumDesktopTests
{
    public class AppiumDesktopTests
    {
        private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        private const string appLocation = @"C:\Users\Gergana\Downloads\ExamPrepResources\ShortURL-DesktopClient-v1.0.net6\ShortURL-DesktopClient.exe";
        private const string myAppLocation = "https://shorturl-2.gerganatsirkova.repl.co/api";
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions appiumOptions;

        [OneTimeSetUp]
        public void OpenApp()
        {
            this.appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptions.AddAdditionalCapability("app", appLocation);
            this.driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            var enterMyAppLocation = driver.FindElement(By.XPath("*//Edit[@Name='URL text box'][@AutomationId='textBoxApiUrl']"));
            enterMyAppLocation.SendKeys(myAppLocation);
            var button = driver.FindElement(By.XPath("*//Button[@AutomationId='buttonConnect']"));
            button.Click(); 
        }

        [OneTimeTearDown]
        public void CloseApp()
        {
            driver.Dispose();
        }

        [Test]
        public void addURL()
        {
            string newURL = "https://softuni.bg";
            var btnAdd = driver.FindElement(By.XPath("*//Button[@AutomationId='buttonAdd']"));
            btnAdd.Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var textBoxUrl = wait.Until(d => {
                return d.FindElement(By.XPath("*//Edit[@Name='url box'][@AutomationId='textBoxURL']"));
            });
            textBoxUrl.SendKeys(newURL);
            var addNewURLBtn = driver.FindElement(By.XPath("*//Button[@AutomationId='buttonCreate']"));
            addNewURLBtn.Click();
            var expectedElement = wait.Until(d => {
                return d.FindElement(By.XPath("*//Text[@Name='https://softuni.bg']"));
            });
            Assert.True(driver.PageSource.Contains(expectedElement.Text));
        }
    }
}