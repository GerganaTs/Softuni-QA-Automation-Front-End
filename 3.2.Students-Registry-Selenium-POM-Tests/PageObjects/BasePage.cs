using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students_Registry_Selenium_POM_Tests.PageObjects
{
    public class BasePage
    {

        private  WebDriver driver;

        // constructor accepts one instance of the browser
        public BasePage(WebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);            
        }


        // baseurl is not set because it is different for every page
        public virtual string PageUrl { get; }



        // define all the fields that we need in order to test the page - we have to define them like properties
        public IWebElement LinkHomePage => driver.FindElement(By.LinkText("Home"));
        public IWebElement LinkViewStudentsPage => driver.FindElement(By.LinkText("View Students"));
        public IWebElement LinkAddStudentPage => driver.FindElement(By.LinkText("Add Student"));
        public IWebElement ElementPageHeading => driver.FindElement(By.CssSelector("body > h1"));



        // Next we have to create the methods (e.g. the actions)

        // First action is to open the browser
        public void Open()
        {
            driver.Navigate().GoToUrl(this.PageUrl);
        }

        // check if we are on the right page
        public bool IsOpen()
        {
            return driver.Url == this.PageUrl;
        }

        // Action get the page title
        public string GetPageTitle()
        {
            return driver.Title;
        }

        // get the heading title of every page
        public string GetPageHeadingTitle()
        {
            return ElementPageHeading.Text;
        }

    }
}
