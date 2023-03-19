using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace DataDrivenTests
{
    public class WebDriverTests
    {
        // promenliva
        private WebDriver driver;
        IWebElement firstInput;
        IWebElement secondtInput;
        IWebElement operationSelect;
        IWebElement button;
        IWebElement resetButton;
        IWebElement resultField;

        [OneTimeSetUp]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            // navigate to wikipedia
            driver.Navigate().GoToUrl("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/");
            driver.Manage().Window.Maximize();

            firstInput = driver.FindElement(By.Id("number1"));
            secondtInput = driver.FindElement(By.Id("number2"));
            operationSelect = driver.FindElement(By.Id("operation"));
            button = driver.FindElement(By.Id("calcButton"));
            resetButton = driver.FindElement(By.Id("resetButton"));
            resultField = driver.FindElement(By.Id("result"));
        }

        [OneTimeTearDown]
        public void CLoseBrowser()
        {
            driver.Quit();
        }

        [TestCase("15", "+", "7", "Result: 22")]
        [TestCase("39", "-", "9", "Result: 30")]
        [TestCase("Hello", "+", "3", "Result: invalid input")]
        

        public void Test_addTwoNumbers_Valid(string firstNum, string operation, string secondNum,string expectedResult)
        {
            // Arrange
            firstInput.SendKeys(firstNum);
            operationSelect.SendKeys(operation);
            secondtInput.SendKeys(secondNum);
            // Act
            button.Click();
            // Result
            Assert.That(resultField.Text, Is.EqualTo(expectedResult));

            resetButton.Click();
        }
    }
}