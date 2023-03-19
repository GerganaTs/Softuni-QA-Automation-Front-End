using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using _3._1.CalculatorPOM.Pages;
using System.Collections.Generic;

namespace _3._1.CalculatorPOM.Tests
{
    public class BaseTests
    {
        public WebDriver driver;

        [SetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        [TearDown]
        public void CLoseBrowser()
        {
            driver.Quit();
        }
    }
}
