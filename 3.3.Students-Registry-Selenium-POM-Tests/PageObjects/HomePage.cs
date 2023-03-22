using OpenQA.Selenium;

namespace StudentsRegistrySeleniumPOMTests.PageObjects
{
    public class HomePage : BasePage
    {
        // constructor:
        public HomePage(WebDriver driver) : base(driver)
        {
        }

        // set the link
        public override string PageUrl => "https://studentregistry.softuniqa.repl.co/";

        // create the property that contains the count of the students
        public IWebElement ElementStudentsCount => driver.FindElement(By.CssSelector("body > p > b"));

        // Action get the page title
        public string GetStudentsCount()
        {
            return ElementStudentsCount.Text;
        }
    }
}