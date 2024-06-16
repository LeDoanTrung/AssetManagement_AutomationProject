using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AssetManagement.Library
{
    public class Element
    {
        public By By { get; set; }

        //Constructor
        public Element(By by)
        {
            By = by;
        }

        //Method
        public IWebElement WaitForElementToVisible()
        {
            return BrowserFactory.Wait.Until(ExpectedConditions.ElementIsVisible(this.By));
        }

        public IWebElement WaitForElementToBeClickable()
        {
            return BrowserFactory.Wait.Until(ExpectedConditions.ElementToBeClickable(this.By));
        }

        public bool IsElementDisplayed()
        {
            IWebElement element = BrowserFactory.Wait.Until(ExpectedConditions.ElementIsVisible(this.By));
            if (element != null)
            {
                return BrowserFactory.Wait.Until(ExpectedConditions.ElementIsVisible(this.By)).Displayed;
            }
            return false;
        }

        public bool IsElementEnabled()
        {
            IWebElement element = BrowserFactory.Wait.Until(ExpectedConditions.ElementExists(this.By));

            if (element.Displayed && element.Enabled)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ClickOnElement()
        {
            IWebElement element = WaitForElementToBeClickable();
            ScrollHelper.ScrollToElement(BrowserFactory.WebDriver, this);
            ((IJavaScriptExecutor)BrowserFactory.WebDriver).ExecuteScript("arguments[0].click();", element);
        }

        public void ClearText()
        {
            var element = WaitForElementToVisible();
            element.Clear();
        }

        public void InputText(string text)
        {
            var element = WaitForElementToVisible();
            element.SendKeys(text);
        }

        public void VerifyMessage(string expectedMessage)
        {
            var message = WaitForElementToVisible().Text;
            Assert.AreEqual(expectedMessage, message);
        }

        public string GetText()
        {
            var text = WaitForElementToVisible().Text;
            return text;
        }

        public string GetValue()
        {
            return WaitForElementToVisible().GetAttribute("value");
        }

        public void SendKeys(string keys)
        {
            var element = WaitForElementToVisible();
            element.SendKeys(keys);
        }
        public void SelectOptionByText(string option)
        {
            IWebElement element = BrowserFactory.WebDriver.FindElement(this.By);
            SelectElement select = new SelectElement(element);
            select.SelectByText(option);
        }

        public void UploadFile(string filePath)
        {
            var element = WaitForElementToVisible();
            element.SendKeys(filePath);
        }

        public IWebElement GetWebElement(IWebDriver driver)
        {
            try
            {

                return driver.FindElement(By);
            }
            catch (NoSuchElementException ex)
            {

                Console.WriteLine("Element not found: " + ex.Message);
                return null;
            }
        }


        public bool IsElementExist()
        {
            try
            {
                IWebElement element = BrowserFactory.WebDriver.FindElement(this.By);
                return element != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsDisabled()
        {
            IWebElement element = WaitForElementToVisible();
            return element.GetAttribute("disabled") != null;
        }
    }
}
