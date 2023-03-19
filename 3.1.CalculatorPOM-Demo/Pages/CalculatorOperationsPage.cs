using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._1.CalculatorPOM.Pages
{
    // create class
    public class CalculatorOperationsPage
    {
        private WebDriver driver;
        private const string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";

        // constructor that recieves one object - browser (this is one time)
        public CalculatorOperationsPage(WebDriver driver)
        {
            this.driver = driver;
        }

        // define all the fields that we need in order to test the page - we have to define them like properties
        public IWebElement FieldNum1 => driver.FindElement(By.Id("number1"));
        public IWebElement FieldNum2 => driver.FindElement(By.Id("number2"));
        public IWebElement FieldOperation => driver.FindElement(By.Id("operation"));
        public IWebElement CalcButton => driver.FindElement(By.Id("calcButton"));
        public IWebElement ResetButton => driver.FindElement(By.Id("resetButton"));
        public IWebElement ResultLabel => driver.FindElement(By.Id("result"));

        // Next we have to create the methods (e.g. the actions)
        
        // Method (action) open the browser
        public void Open() 
        {  
            driver.Navigate().GoToUrl(BaseUrl);
        }

        // Method (action) get the page title that returns the driver title
        public string GetPageTitle()
        {
            return driver.Title;
        }

        // Method (action) check if we are on the right page
        public bool IsOpen()
        {
            return driver.Url == BaseUrl;
        }

        // Method (action) make some operations
        public string CalculateNumbers(string firstValue, string operation, string secondValue)
        {
            FieldNum1.SendKeys(firstValue);
            FieldOperation.SendKeys(operation);
            FieldNum2.SendKeys(secondValue);
            CalcButton.Click();
            return ResultLabel.Text;
        }

        // Method (action) reset form
        public bool IsFormReset()
        {
            bool resetResult = (FieldNum1.Text == "") && (FieldNum2.Text == "") && (ResultLabel.Text == "");
            return resetResult;
        }
    }

}
