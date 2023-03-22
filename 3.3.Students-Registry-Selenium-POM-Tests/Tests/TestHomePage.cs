using OpenQA.Selenium;
using StudentsRegistrySeleniumPOMTests.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsRegistrySeleniumPOMTests.Tests
{
    public class TestHomePage : BaseTests
    {
        //this is the current page object that must be created for every page
        private HomePage page;

        public void Setup()
        {
            this.page = new HomePage(driver);
        }

        [Test]
        public void Test_HomePage_Content()
        {
            page.Open();
            Assert.That(page.GetPageTitle(), Is.EqualTo("MVC Example"));
        }

        [Test]
        public void Test_HomePage_Heading()
        {
            page.Open();
            Assert.That(page.GetPageHeadingTitle(), Is.EqualTo("Students Registry"));
        }
        [Test]
        public void Test_HomePage_StudentsCount()
        {
            page.Open();
            Assert.IsNotNull(page.GetStudentsCount());
        }
    }
}
