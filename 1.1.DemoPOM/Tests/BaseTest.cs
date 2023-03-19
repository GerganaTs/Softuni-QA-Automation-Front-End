using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace _1._1.DemoPOM.Tests
{
    public class BaseTest
    {
        public WebDriver driver;

        [SetUp]

        public void Setup()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]

        public void TearDown() 
        {
            driver.Quit();
        }
    }
}
