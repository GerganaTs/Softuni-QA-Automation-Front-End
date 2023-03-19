using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Homework1
{
    public class TestsEx1
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void TestSearchQainWikipedia()
        {
            driver.Url = "https://wikipedia.org";
            driver.FindElement(By.Id("searchInput")).Click();
            driver.FindElement(By.Id("searchInput")).SendKeys("QA");
            driver.FindElement(By.Id("searchInput")).SendKeys(Keys.Enter);
            Assert.AreEqual(driver.Url, "https://en.wikipedia.org/wiki/QA");
        }
    }
}