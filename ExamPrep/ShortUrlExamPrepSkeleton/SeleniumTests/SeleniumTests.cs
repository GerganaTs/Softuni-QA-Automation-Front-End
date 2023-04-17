using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SeleniumTests
{
    public class SeleniumTests
    {
        private WebDriver driver;
        private const string BaseUrl = "https://shorturl-1.gerganatsirkova.repl.co";

        [OneTimeSetUp]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(BaseUrl);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);            
        }

        [OneTimeTearDown]
        public void CLoseBrowser()
        {
            driver.Quit();
        }

        public void addNewURL(string newUrl)
        {
            var addURL = driver.FindElement(By.LinkText("Add URL"));
            addURL.Click();
            var newLinkText = "https://softuni.bg";
            var inputURL = driver.FindElement(By.CssSelector("input.url"));
            var createURLButton = driver.FindElement(By.CssSelector("button[type=\"submit\"]"));
            inputURL.SendKeys(newLinkText);
            createURLButton.Click();
        }

        public int getURLCounter()
        {
            driver.Navigate().GoToUrl(BaseUrl);
            var counter = driver.FindElement(By.CssSelector("ul>li:nth-child(1) b"));

            return Int32.Parse(counter.Text);
        }

        [Test, Order(1)]
        public void OpenShortURLPage_Test_TopLeftCell()
        {
            var shortURLPage = driver.FindElement(By.LinkText("Short URLs"));
            shortURLPage.Click();
            var topLeftThElement = driver.FindElement(By.CssSelector("thead>tr>th:nth-child(1)"));
            Assert.That(topLeftThElement.Text, Is.EqualTo("Original URL"));
        }

        [Test, Order(2)]
        public void OpenShortURLPage_Test_AddNewUrl_Valid()
        {
            addNewURL("https://softuni.bg");
            var newLinkText = "https://softuni.bg";
            var newAddedLink = driver.FindElement(By.CssSelector("tbody > tr:last-of-type > td:nth-child(1) > a"));
            Assert.That(newAddedLink.Text, Is.EqualTo(newLinkText));
        }

        [Test, Order(3)]
        public void OpenShortURLPage_Test_AddNewUrl_Invalid()
        {
            var addURL = driver.FindElement(By.LinkText("Add URL"));
            addURL.Click();
            var invalidUrlText = "Invalid URL!";
            var inputURL = driver.FindElement(By.CssSelector("input.url"));
            var createURLButton = driver.FindElement(By.CssSelector("button[type=\"submit\"]"));
            inputURL.SendKeys("softuni.bg");
            createURLButton.Click();
            var errorMessage = driver.FindElement(By.ClassName("err"));
            Assert.That(errorMessage.Text, Is.EqualTo(invalidUrlText));
            Assert.True(errorMessage.Displayed);
        }

        [Test, Order(4)]
        public void OpenShortURLPage_Test_AddNewUrl_InvalidPage()
        {
            driver.Navigate().GoToUrl("https://shorturl.nakov.repl.co/go/invalid536524");
            var errorMessage = driver.FindElement(By.ClassName("err"));
            var errorHeader = driver.FindElement(By.CssSelector("h1"));
            var errorText = driver.FindElement(By.CssSelector("p"));

            Assert.That(errorMessage.Text, Is.EqualTo("Cannot navigate to given short URL"));
            Assert.True(errorMessage.Displayed);
            Assert.That(errorHeader.Text, Is.EqualTo("Error: Cannot navigate to given short URL"));
            Assert.True(errorHeader.Displayed);
            Assert.That(errorText.Text, Is.EqualTo("Invalid short URL code: invalid536524"));
            Assert.True(errorText.Displayed);
        }

        [Test, Order(5)]
        public void OpenShortURLPage_Test_Counter()
        {
            int countBefore = getURLCounter() + 1;
            addNewURL("https://softuni.bg");
            int countAfter = getURLCounter();
            Assert.That(countBefore, Is.EqualTo(countAfter));
        }
    }
}