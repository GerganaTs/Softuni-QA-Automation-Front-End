using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace _2._1.Homework
{
    public class WebDriverTests
    {
        private WebDriver driver;
        private const string BaseUrl = "https://shorturl.softuniqa.repl.co/urls";

        [OneTimeSetUp]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(BaseUrl);
        }

        [OneTimeTearDown]
        public void CLoseBrowser()
        {
            driver.Quit();
        }

        [Test]
        public void TestTitle()
        {
            var pageTitle = driver.FindElement(By.CssSelector("body > main > h1"));
            var pageTitleResult = "Short URLs";
            Assert.That(pageTitle.Text, Is.EqualTo(pageTitleResult));
        }

        [Test]
        public void TestTableCellFirst()
        {
            var nakovCellLink = driver.FindElement(By.LinkText("https://nakov.com"));
            var result = "https://nakov.com";
            Assert.That(nakovCellLink.Text, Is.EqualTo(result));
        }

        [Test]
        public void TestTableCellSecond()
        {
            var nakovCellLink = driver.FindElement(By.LinkText("http://shorturl.softuniqa.repl.co/go/nak"));
            var result = "http://shorturl.softuniqa.repl.co/go/nak";
            Assert.That(nakovCellLink.Text, Is.EqualTo(result));
        }
    }
}