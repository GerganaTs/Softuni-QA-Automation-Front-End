using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace SeleniumWebDriverDemo1._2
{
    public class WebDriverTests
    {
        private WebDriver driver;
        
        [OneTimeSetUp]
        public void OpenBrowser()
        {
            
            
            
            this.driver = new ChromeDriver();
            // navigate to wikipedia
            driver.Navigate().GoToUrl("https://wikipedia.org");
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]

        public void CLoseBrowser()
        {
            this.driver.Quit();
        }

        [Test]

        public void wikipediaTitle()
        {
            // Get browser Title
            var pageTitle = driver.Title;
            Assert.That(driver.Title, Is.EqualTo(pageTitle));
        }
        [Test]
        public void testWikipediaSearch()
        {
            var pageTitle = driver.Title;
            driver.FindElement(By.Id("searchInput")).Click();
            driver.FindElement(By.Id("searchInput")).SendKeys("QA");
            driver.FindElement(By.Id("searchInput")).SendKeys(Keys.Enter);
            Assert.That(driver.FindElement(By.CssSelector(".mw-page-title-main")).Text, Is.EqualTo("QA"));
            Thread.Sleep(3000);
            driver.Navigate().Back();
            Assert.That(pageTitle, Is.EqualTo("Wikipedia"));
        }
    }
}