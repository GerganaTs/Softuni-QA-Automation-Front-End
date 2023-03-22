using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace StudentsRegistrySeleniumPOMTests.PageObjects
{
    public class ViewStudentsPage : BasePage
    {
        // constructor:
        public ViewStudentsPage(WebDriver driver) : base(driver)
        {
        }

        // set the link
        public override string PageUrl => "https://studentregistry.softuniqa.repl.co/students";

        // create the property that contains the count of the students
        public ReadOnlyCollection<IWebElement> listItemsStudents => driver.FindElements(By.CssSelector("body ul > li"));

        public string[] GetRegisteredStudents() 
        {
            var elementsStudents = this.listItemsStudents.Select(s => s.Text).ToArray();
            return elementsStudents;
        }
    }
}