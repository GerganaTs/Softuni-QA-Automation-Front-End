using StudentsRegistrySeleniumPOMTests.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsRegistrySeleniumPOMTests.Tests
{
    public class TestViewStudentsPage : BaseTests
    {
        //this is the current page object that must be created for every page
        private ViewStudentsPage page;

        public void Setup()
        {
            this.page = new ViewStudentsPage(driver);
        }

        [Test]
        public void Test_HomePage_CheckTilte()
        {
            page.Open();
            Assert.That(page.GetPageTitle(), Is.EqualTo("Students"));
        }
    }
}
