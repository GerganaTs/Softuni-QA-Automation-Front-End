

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Linq;

namespace SeleniumTests
{
    public class SeleniumTests
    {
        private WebDriver driver;
        private const string baseUrl = "https://contactbook.gerganatsirkova.repl.co/";

        private IWebElement homePageLink => driver.FindElement(By.LinkText("Home")) ;
        private IWebElement contactsPageLink => driver.FindElement(By.LinkText("Contacts")) ;
        private IWebElement searchLink => driver.FindElement(By.LinkText("Search")) ;
        private IWebElement createContactPageLink => driver.FindElement(By.LinkText("Create"));

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(baseUrl);
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void CLoseBrowser()
        {
            driver.Quit();
        }

        public void SearchByString(string SearchedContactString)
        {
            homePageLink.Click();
            searchLink.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var searchInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("keyword")));
            searchInput.SendKeys(SearchedContactString);
            var searchButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("search")));
            searchButton.Click();
        }

        [Test, Order(1)]
        public void Test_ContactPage_FirstContact()
        {
            homePageLink.Click();
            contactsPageLink.Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var firstName = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#contact1 .fname td")));
            var lastName = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#contact1 .lname td")));
            var wholeName = firstName.Text + " " + lastName.Text;
            Assert.That(firstName.Text, Is.EqualTo("Steve"));
            Assert.That(lastName.Text, Is.EqualTo("Jobs"));
            Assert.That(wholeName, Is.EqualTo("Steve Jobs"));
        }


        [Test, Order(2)]
        public void Test_SearchPage_SearchAlbert()
        {
            SearchByString("albert");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var firstContactFirstName = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".contacts-grid > a:first-child tr.fname > td")));
            var firstContactLastName = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".contacts-grid > a:first-child tr.lname > td")));
            var wholeName = firstContactFirstName.Text + " " + firstContactLastName.Text;
            Assert.That(firstContactFirstName.Text, Is.EqualTo("Albert"));
            Assert.That(firstContactLastName.Text, Is.EqualTo("Einstein"));
            Assert.That(wholeName, Is.EqualTo("Albert Einstein"));
        }


        [Test, Order(3)]
        public void Test_SearchPage_SearchNotExistingContact()
        {
            SearchByString("missing{randnum}");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var notFoundMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("searchResult")));
            Assert.True(notFoundMessage.Displayed);
            Assert.That(notFoundMessage.Text, Is.EqualTo("No contacts found."));
        }

        public void CreateContact(string firstName, string lastName, string email, string phone, string comments)
        {
            homePageLink.Click();
            createContactPageLink.Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var firstNameInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("firstName")));
            firstNameInput.SendKeys(firstName);
            var lastNameInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("lastName")));
            lastNameInput.SendKeys(lastName);
            var emailInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
            emailInput.SendKeys(email);
            var phoneInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("phone")));
            phoneInput.SendKeys(phone);
            var commentInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("comments")));
            commentInput.SendKeys(comments);

            var buttonCreate = driver.FindElement(By.Id("create"));
            buttonCreate.Click();
        }


        [Test, Order(4)]
        public void Test_CreateContactPage_CreateContactInvalidData()
        {
            CreateContact(" ", "Ivanov", "test@gmail.com", "0888888888", "Test Comment goes here");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".err")));
            Assert.True(errorMessage.Displayed);
            Assert.That(errorMessage.Text, Is.EqualTo("Error: First name cannot be empty!"));
        }


        [Test, Order(5)]
        public void Test_CreateContactPage_CreateContactValidData()
        {
            var fname = "Ivan";
            var lname = "Ivanov";
            var email = "test@gmail.com";
            var phone = "0882597071";
            var comment = "Test Comment goes here";
            CreateContact(fname, lname, email, phone, comment);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var createdUserFirstName = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".contacts-grid > a:last-child .fname td")));
            var createdUserLastnameName = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".contacts-grid > a:last-child .lname td")));
            var createdUserEmail = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".contacts-grid > a:last-child .email td")));
            var createdUserPhone = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".contacts-grid > a:last-child .phone td")));
            var createdUserComments = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".contacts-grid > a:last-child div.comments")));
            var html = driver.PageSource;

            Assert.True(createdUserFirstName.Displayed);
            Assert.True(html.Contains(createdUserFirstName.Text));
            Assert.That(createdUserFirstName.Text, Is.EqualTo(fname));

            Assert.True(createdUserLastnameName.Displayed);
            Assert.True(html.Contains(createdUserLastnameName.Text));
            Assert.That(createdUserLastnameName.Text, Is.EqualTo(lname));

            Assert.True(createdUserEmail.Displayed);
            Assert.True(html.Contains(createdUserEmail.Text));
            Assert.That(createdUserEmail.Text, Is.EqualTo(email));

            Assert.True(createdUserPhone.Displayed);
            Assert.True(html.Contains(createdUserPhone.Text));
            Assert.That(createdUserPhone.Text, Is.EqualTo(phone));

            Assert.True(createdUserComments.Displayed);
            Assert.True(html.Contains(createdUserComments.Text));
            Assert.That(createdUserComments.Text.Trim, Is.EqualTo(comment));
        }
    }
}