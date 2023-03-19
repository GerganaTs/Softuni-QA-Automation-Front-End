using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace DataDrivenTests
{
    public class WebDriverTests
    {
        private WebDriver driver;
        private const string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";

        IWebElement firstInput;
        IWebElement secondtInput;
        IWebElement button;
        IWebElement resetButton;
        IWebElement resultField;

        [OneTimeSetUp]
        public void OpenBrowser()
        {
            // headless mode
            var options = new ChromeOptions();
            options.AddArguments("--headless", "--lang=en_US");
            driver = new ChromeDriver(options);
            // end headless mode else:
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(BaseUrl);

            // Implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            // End of Implicit wait

            firstInput = driver.FindElement(By.Id("number1"));
            secondtInput = driver.FindElement(By.Id("number2"));
            button = driver.FindElement(By.Id("calcButton"));
            resetButton = driver.FindElement(By.Id("resetButton"));
            resultField = driver.FindElement(By.Id("result"));
        }

        [OneTimeTearDown]
        public void CLoseBrowser()
        {
            driver.Quit();
        }

        [TestCase("15", "7", "Result: 22")]
        [TestCase("39", "9", "Result: 30")]

        public void Test_addTwoNumbers_Valid(string firstNum, string secondNum, string result)
        {
            // Explicit wait
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var loginButton = wait.Until<IWebElement>(driver =>
            {
                return driver.FindElement(By.LinkText("Login"));
            });
            // End of Implicit wait

            // Arrange
            resetButton.Click();
            firstInput.SendKeys(firstNum);
            secondtInput.SendKeys(secondNum);
            // Act
            button.Click();
            // Result
            Assert.That(resultField.Text, Is.EqualTo(result));
        }
    }
}