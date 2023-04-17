using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace AndroidAppimTests
{
    public class Tests
    {
        public const string appiumUrl = "http://127.0.1:4723/wd/hub";
        public const string appLocation = @"C:\Apps\softuni\com.example.androidappsummator.apk";
        private AndroidDriver<AndroidElement> driver;
        public AppiumOptions appiumOptions;

        [OneTimeSetUp]
        public void Setup()
        {
            this.appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", appLocation);
            appiumOptions.AddAdditionalCapability("PlatformName", "Android");
            appiumOptions.AddAdditionalCapability("DeviceName", "Nexus_6_API_30");

            this.driver = new AndroidDriver<AndroidElement>(new Uri(appiumUrl), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        [OneTimeTearDown]

        public void TearDown()
        {
            driver.Dispose();
        }

        [Test]
        public void TestCalculate_TwoPositiveNumbers()
        {
            var firstInput = driver.FindElementById("com.example.androidappsummator:id/editText1");
            var secondInput = driver.FindElementById("com.example.androidappsummator:id/editText2");
            var resultField = driver.FindElementById("com.example.androidappsummator:id/editTextSum");
            var buttonCalc = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
            firstInput.Clear();
            secondInput.Clear();
            firstInput.SendKeys("5");
            secondInput.SendKeys("16");
            buttonCalc.Click();

            var result = resultField.Text;

            Assert.That(result, Is.EqualTo("21"));
        }

        [Test]
        public void TestCalculate_InvalidData()
        {
            var firstInput = driver.FindElementById("com.example.androidappsummator:id/editText1");
            var secondInput = driver.FindElementById("com.example.androidappsummator:id/editText2");
            var resultField = driver.FindElementById("com.example.androidappsummator:id/editTextSum");
            var buttonCalc = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");

            firstInput.Clear();
            secondInput.Clear();
            firstInput.SendKeys("error");
            secondInput.SendKeys("16");
            buttonCalc.Click();

            var result = resultField.Text;

            Assert.That(result, Is.EqualTo("error"));
        }
    }
}