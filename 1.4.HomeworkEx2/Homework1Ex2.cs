using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Homework1
{
    public class TestsHomework
    {
        // promenliva
        private static IWebDriver driver;

        [OneTimeSetUp]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void Test_addTwoNumbers_Valid()
        {
            driver.Url = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";
            // input first number
            driver.FindElement(By.Id("number1")).SendKeys("15");
            // select type of operation
            driver.FindElement(By.Name("operation")).Click();
            driver.FindElement(By.CssSelector("#operation > option:nth-child(2)")).Click();
            // input second number
            driver.FindElement(By.Id("number2")).SendKeys("7");
            // click calculate button
            driver.FindElement(By.Id("calcButton")).Click();
            Assert.That(driver.FindElement(By.CssSelector("#result > pre")).Text, Is.EqualTo("22"));
            Thread.Sleep(3000);
        }

        [Test]
        public void Test_addTwoNumbers_InvalidInput()
        {
            driver.Url = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";
            // input first number
            driver.FindElement(By.Id("number1")).SendKeys("Hello");
            // select type of operation
            driver.FindElement(By.Name("operation")).Click();
            driver.FindElement(By.CssSelector("#operation > option:nth-child(2)")).Click();
            // input second number
            driver.FindElement(By.Id("number2")).SendKeys("");
            // click calculate button
            driver.FindElement(By.Id("calcButton")).Click();
            Assert.That(driver.FindElement(By.CssSelector("#result > i")).Text, Is.EqualTo("invalid input"));
            Thread.Sleep(3000);
        }

        [Test]
        public void Test_addTwoNumbers_Reset()
        {
            driver.Url = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";
            // input first number
            driver.FindElement(By.Id("number1")).SendKeys("Hello");
            // select type of operation
            driver.FindElement(By.Name("operation")).Click();
            driver.FindElement(By.CssSelector("#operation > option:nth-child(2)")).Click();
            // input second number
            driver.FindElement(By.Id("number2")).SendKeys("Hello");
            // click calculate button
            driver.FindElement(By.Id("calcButton")).Click();
            // click reset button
            driver.FindElement(By.Id("resetButton")).Click();
            Assert.That(driver.FindElement(By.CssSelector("#number1")).Text, Is.EqualTo(""));
            Assert.That(driver.FindElement(By.CssSelector("#operation > option:nth-child(1)")).Text, Is.EqualTo("-- select an operation --"));
            Assert.That(driver.FindElement(By.CssSelector("#number2")).Text, Is.EqualTo(""));
            Thread.Sleep(3000);
        }
    }
}