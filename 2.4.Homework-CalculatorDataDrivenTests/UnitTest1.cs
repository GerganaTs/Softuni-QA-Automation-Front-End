using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace DataDrivenTests
{
    public class WebDriverTests
    {
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

        [TestCase("", "-", "3", "Result: invalid input")]
        [TestCase("", "*", "3", "Result: invalid input")]
        [TestCase("", "/", "3", "Result: invalid input")]
        [TestCase("", "/+", "3", "Result: invalid input")]
        [TestCase("1.5e53", "*", "150", "Result: 2.25e+55")]
        [TestCase("1.5e53", "/", "150", "Result: 2.25e+55")]
        [TestCase("12", "/", "3", "Result: 4")]
        [TestCase("12.5", "/", "4", "Result: 3.125")]
        [TestCase("32", "-", "infinity", "Result: -infinity")]
        [TestCase("3", "!!!!!!!", "7", "Result: invalid operation")]
        


        public void Test_addTwoNumbers_Valid(string firstNum, string operation, string secondNum, string expectedResult)
        {
            resetButton.Click();
            // Arrange
            firstInput.SendKeys(firstNum);
            operationSelect.SendKeys(operation);
            secondtInput.SendKeys(secondNum);
            // Act
            button.Click();
            // Result
            Assert.That(resultField.Text, Is.EqualTo(expectedResult));
        }
    }
}