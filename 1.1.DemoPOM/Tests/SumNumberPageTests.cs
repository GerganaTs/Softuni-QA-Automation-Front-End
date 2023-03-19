using _1._1.DemoPOM.Pages;

namespace _1._1.DemoPOM.Tests
{
    public class POMTests : BaseTest
    {
        private SumNumbersPage page;

        [SetUp]
        public void SetupTests()
        {
            this.page = new SumNumbersPage(driver);
            page.Open();
        }

        [Test]
        public void Test_SumNumbersPage_CheckTitle()
        {           
            Assert.That(page.GetPageTitle(), Is.EqualTo("Number Calculator"));
        }

        [TestCase("", "-", "3", "Result: invalid input")]
        [TestCase("", "*", "3", "Result: invalid input")]
        [TestCase("", "/", "3", "Result: invalid input")]
        [TestCase("", "/+", "3", "Result: invalid input")]
        [TestCase("1.5e53", "*", "150", "Result: 2.25e+55")]
        [TestCase("1.5e53", "/", "150", "Result: 1e+51")]
        [TestCase("12", "/", "3", "Result: 4")]
        [TestCase("12.5", "/", "4", "Result: 3.125")]
        [TestCase("32", "-", "infinity", "Result: invalid input")]
        [TestCase("3", "!!!!!!!", "7", "Result: invalid operation")]
        /*[TestCase("32f", "+", "32", "Result: invalid input")]
        [TestCase("32f", "-", "32", "Result: invalid input")]
        [TestCase("32f", "*", "32", "Result: invalid input")]
        [TestCase("32f", "/", "32", "Result: invalid input")]*/
        [TestCase("32", "-", "infinity", "Result: invalid input")]
        public void Test_SumNumbersPage_Tests(string num1, string operation, string num2, string result)
        {
            var actualResult = page.CalculateNumbers(num1, operation, num2);
            Assert.That(actualResult, Is.EqualTo(result));
        }

        [Test]
        public void ResetForm()
        {
            page.ResetButton.Click();
        }
    }
}