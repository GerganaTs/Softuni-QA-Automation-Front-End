using OpenQA.Selenium;
using Students_Registry_Selenium_POM_Tests.PageObjects;

namespace Students_Registry_Selenium_POM_Tests
{
    public class HomePage : BasePage
    {
        // constructor:
        public HomePage(WebDriver driver) : base(driver)
        {
        }

        // set the link
        public override string PageURL => "https://studentregistry.softuniqa.repl.co/";

        // create the property that contains the count of the students
        public IWebElement ElementStudentsCount => driver.FindElement(By.CssSelector("body > p > b"));
    }
}