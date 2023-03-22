using NUnit.Framework.Internal;
using OpenQA.Selenium;

namespace StudentsRegistrySeleniumPOMTests.PageObjects
{
    public class AddStudentsPage : BasePage
    {
        // constructor:
        public AddStudentsPage(WebDriver driver) : base(driver)
        {
            
        }
        
        public IWebElement Name => driver.FindElement(By.Id("name"));
        public IWebElement Email => driver.FindElement(By.Id("email"));
        public IWebElement Button => driver.FindElement(By.Id("button[type=submit]"));
        public IWebElement ErrMessage => driver.FindElement(By.CssSelector("body > div"));

        // set the link
        public override string PageUrl => "https://studentregistry.softuniqa.repl.co/add-student";

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public string GetErrorMsg()
        {
            var result = ErrMessage.Text;
            return result;
        }
        
        public void AddStudent(string name, string email)
        {
            Name.SendKeys(name);
            Email.SendKeys(email);
            Button.Click();
        }
    }
}