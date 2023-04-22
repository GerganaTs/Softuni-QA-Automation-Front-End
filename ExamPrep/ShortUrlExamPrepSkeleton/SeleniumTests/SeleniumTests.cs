using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = OpenQA.Selenium.Support.UI.ExpectedConditions;

namespace SeleniumTests
{
    public class SeleniumTests
    {
        private WebDriver driver;
        private const string BaseUrl = "https://shorturl-3.gerganatsirkova.repl.co";
        private const string ShortUrl = "https://shorturl-3.gerganatsirkova.repl.co/urls";
        private const string AddURL = "https://shorturl-3.gerganatsirkova.repl.co/add-url";

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(BaseUrl);
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void CLoseBrowser()
        {
            driver.Quit();
        }

        public void addURL(string urlToAdd)
        {
            driver.Navigate().GoToUrl(AddURL);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var addUrlField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("url")));
            var addUrButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[type='submit']")));
            addUrlField.SendKeys(urlToAdd);
            addUrButton.Click();
        }

        [Test, Order(1)]
        public void Test_Homepage_OriginalURL()
        {
            driver.Navigate().GoToUrl(ShortUrl);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var originalURL = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("table > thead > tr > th:nth-child(1)")));
            Assert.That(originalURL.Text, Is.EqualTo("Original URL"));
        }

        [Test, Order(2)]
        public void Test_AddUrlPage_HappyPath()
        {
            var url = "https://softuni.bg";
            addURL(url);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var lastValidUrlEntered = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("table > tbody > tr:last-child > td:first-child")));
            Assert.That(driver.Url, Is.EqualTo(ShortUrl));
            Assert.That(lastValidUrlEntered.Text, Is.EqualTo(url));
        }

        [Test, Order(3)]
        public void Test_AddUrlPage_NegativeCase()
        {
            var url = "softuni.bg";
            addURL(url);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var error = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.err")));
            Assert.True(error.Displayed);
            Assert.That(error.Text, Is.EqualTo("Invalid URL!"));
            var addUrlField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("url")));
            addUrlField.Clear();
        }

        [Test, Order(4)]
        public void Test_AddUrlPage_NavigateToNotExistingUrl()
        {
            var url = "http://shorturl.nakov.repl.co/go/invalid536524";
            driver.Navigate().GoToUrl(url);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var error = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.err")));
            Assert.True(error.Displayed);
            Assert.That(error.Text, Is.EqualTo("Cannot navigate to given short URL"));
        }

        [Test, Order(5)]
        public void Test_HomePageCounter()
        {
            driver.Navigate().GoToUrl(ShortUrl);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var counterBefore = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("table > tbody > tr:last-child > td:last-child")));
            var oldCounter = int.Parse(counterBefore.Text);
            var lastAddedUrlVisitorsLink = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("table > tbody > tr:last-child > td:nth-child(2) > a")));          
            lastAddedUrlVisitorsLink.Click();
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            driver.Navigate().GoToUrl(ShortUrl);
            var counterAfter = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("table > tbody > tr:last-child > td:last-child")));
            var newCounter = int.Parse(counterAfter.Text);
            Assert.That(oldCounter+1, Is.EqualTo(newCounter));
        }
    }
}