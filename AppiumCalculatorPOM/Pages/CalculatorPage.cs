global using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumCalculatorPOM.Pages
{
    public class CalculatorPage
    {
        private WindowsDriver<WindowsElement> driver;
        public CalculatorPage(WindowsDriver<WindowsElement> driver)
        {
            this.driver =driver;
        }

        public WindowsElement firstField => driver.FindElementByAccessibilityId("textBoxFirstNum");
        public WindowsElement secondField => driver.FindElementByAccessibilityId("textBoxSecondNum");
        public WindowsElement resultField => driver.FindElementByAccessibilityId("textBoxSum");
        public WindowsElement calcButton => driver.FindElementByAccessibilityId("buttonCalc");

        public string CaclulateNumbers(string firstValue, string secondValue)
        {
            firstField.Clear();
            secondField.Clear();
            firstField.SendKeys(firstValue);
            secondField.SendKeys(secondValue);
            calcButton.Click();

            return resultField.Text;
        }

    }
}
