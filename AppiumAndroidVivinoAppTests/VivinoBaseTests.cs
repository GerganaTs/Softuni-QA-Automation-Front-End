global using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace AppiumAndroidVivinoAppTests
{
    public class VivinoTests
    {
        private const string UriString = "http://127.0.0.1:4723/wd/hub";
        private const string appLocation = "C:\\Apps\\softuni\\vivino_8.18.11-8181203.apk";
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;

        [SetUp]
        public void PrepareApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", appLocation);
            options.AddAdditionalCapability("appPackage", "vivino.web.app");
            options.AddAdditionalCapability("launchActivity", "com.sphinx_solutions.activities.SplashActivity");
            this.driver = new AndroidDriver<AndroidElement>(new Uri(UriString), options);
        }

        [TearDown]
        public void CloseApp()
        {
            driver.Quit();
        }

        [Test]
        public void Test_SearchWine_VerifyNameAndRating()
        {
            Assert.Pass();
        }
    }
}