using OpenQA.Selenium;
using StudentsRegistrySeleniumPOMTests.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StudentsRegistrySeleniumPOMTests.Tests
{
    public class TestAddStudentsPage : BaseTests
    {
        //this is the current page object that must be created for every page
        private AddStudentsPage page;

        public void Setup()
        {
            this.page = new AddStudentsPage(driver);
        }

        [Test]
        public void Test_HomePage_CheckTilte()
        {
            page.Open();
            Assert.That(page.GetPageTitle(), Is.EqualTo("Add Student"));
        }

        [TestCase("Tester Name1", "test@yahoo.com")]
        [TestCase("123456", "test@yahoo.com")]
        [TestCase("$$$!@#", "")]
        [TestCase("Tester", "123456")]
        [TestCase("Tester", "$$$!@#")]
        [TestCase("", "")]
        [TestCase("Ana-Maria", "")]
        [TestCase("", "test@com")]
        [TestCase("-", "test@com")]
        public void AddStudentPositiveCases(string name, string email)
        {
            page.Open();
            var expectedStudent = "{name}({email})";
            var addedStudent = driver.FindElement(By.CssSelector("body > ul > li:last-child"));
            Assert.That(addedStudent.Text, Is.EqualTo(expectedStudent));
        }

        public void GetErrorMsg()
        {
            page.Open();
            Assert.That(page.GetPageTitle(), Is.EqualTo("Cannot add student. Name and email fields are required!"));
        }

    }
}
