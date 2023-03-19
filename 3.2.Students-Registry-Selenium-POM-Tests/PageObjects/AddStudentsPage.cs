using OpenQA.Selenium;
using Students_Registry_Selenium_POM_Tests.PageObjects;

namespace Students_Registry_Selenium_POM_Tests
{
    public class AddStudentPage : BasePage
    {
        // constructor:
        public AddStudentPage(WebDriver driver) : base(driver)
        {
        }

        // set the link
        public override string PageURL => "https://studentregistry.softuniqa.repl.co/add-student";

    }
}