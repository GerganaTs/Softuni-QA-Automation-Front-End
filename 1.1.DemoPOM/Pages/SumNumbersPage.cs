using OpenQA.Selenium;

namespace _1._1.DemoPOM.Pages
{
    internal class SumNumbersPage
    {
        
        private WebDriver driver;
        private const string baseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";

        public SumNumbersPage(WebDriver driver)
        {
            this.driver = driver;
        }

        // for every field we create property
        public IWebElement FieldNum1 => driver.FindElement(By.Id("number1"));
        public IWebElement FieldOperation => driver.FindElement(By.Id("operation"));
        public IWebElement FieldNum2 => driver.FindElement(By.Id("number2"));
        public IWebElement CalcButton => driver.FindElement(By.Id("calcButton"));
        public IWebElement ResetButton => driver.FindElement(By.Id("resetButton"));
        public IWebElement Result => driver.FindElement(By.Id("result"));

        public void Open()
        {
            driver.Navigate().GoToUrl(baseUrl);
        }

        // check the page title
        public string GetPageTitle()
        {
            return driver.Title;
        }

        // check if we are on the right page
        public bool IsPageOpen()
        {
            return driver.Url == baseUrl;
        }

        public string CalculateNumbers (string firstValue, string operation, string secondValue)
        {
            FieldNum1.SendKeys(firstValue);
            FieldOperation.SendKeys(operation);
            FieldNum2.SendKeys(secondValue);       
            CalcButton.Click();
            return Result.Text;
        }
        
    }
}
