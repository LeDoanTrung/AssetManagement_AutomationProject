using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using AssetManagement.Test;
using System;
using OpenQA.Selenium.Interactions;

namespace AssetManagement.Library
{
    public static class DriverUtils
    {
        public static int GetWaitTimeoutSeconds()
        {
            return int.Parse(ConfigurationHelper.GetConfigurationByKey("Timeout.Webdriver.Wait.Seconds"));
        }

        public static void GoToUrl(string url)
        {
            BaseTest.GetWebDriver().Url = url;
            BaseTest.Node.Pass("Open URL: " + url);
        }

        //Wait Element 
        public static IWebElement WaitForElementToBeVisible(WebObject webObject)
        {
            try
            {
                var wait = new WebDriverWait(BaseTest.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                return wait.Until(ExpectedConditions.ElementIsVisible(webObject.By));
            }
            catch (WebDriverTimeoutException exception)
            {
                var message = $"Element is not visible as expected. Element information: {webObject.Name}";
                BaseTest.Node.Fail(message);
                throw exception;
            }
        }

        public static IWebElement WaitForElementToBeClickable(WebObject webObject)
        {
            try
            {
                var wait = new WebDriverWait(BaseTest.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                return wait.Until(ExpectedConditions.ElementIsVisible(webObject.By));
            }
            catch (WebDriverTimeoutException exception)
            {
                var message = $"Element is not clickable as expected. Element information: {webObject.Name}";
                BaseTest.Node.Fail(message);
                throw exception;
            }
        }

        public static void WaitForElementToBeInvisible(WebObject wobject)
        {
            try
            {
                var wait = new WebDriverWait(BaseTest.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(wobject.By));
            }
            catch (WebDriverTimeoutException)
            {
                var message = $"Element is still visible. Element information: {wobject.Name}";
                Console.WriteLine(message);
                BaseTest.Node.Pass(message);
            }
        }

        public static void WaitForPageLoadCompletely()
        {

            var wait = new WebDriverWait(BaseTest.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
            wait.Until(driver1 => ((IJavaScriptExecutor)BaseTest.GetWebDriver()).ExecuteScript("return document.readyState").Equals("complete"));
        }

        //Get attribute of an Element
        public static bool IsElementDisplayed(WebObject webObject)
        {
            bool result;
            var wait = new WebDriverWait(BaseTest.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
            try
            {
                result = wait.Until(ExpectedConditions.ElementIsVisible(webObject.By)).Displayed;
                Console.WriteLine(webObject.Name + " is displayed as expected");
                BaseTest.Node.Pass(webObject.Name + " is displayed as expected");
            }
            catch (WebDriverTimeoutException)
            {
                result = false;
                Console.WriteLine(webObject.Name + " is not displayed as expected");
                BaseTest.Node.Pass(webObject.Name + " is not displayed as expected");
            }
            return result;
        }

        public static string GetTextFromElement(WebObject webObject)
        {
            try
            {
                var element = WaitForElementToBeVisible(webObject);
                Console.WriteLine("Get text from " + webObject.Name);
                BaseTest.Node.Pass("Get text from " + webObject.Name);
                return element.Text;
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to get text from element. Element information: {webObject.Name}";
                BaseTest.Node.Fail(message);
                throw exception;
            }
        }

        //Action on Element
        public static void ClickOnElement(WebObject webObject)
        {
            try
            {
                var element = WaitForElementToBeClickable(webObject);
                element.Click();
                Console.WriteLine("Click on " + webObject.Name);
                BaseTest.Node.Pass("Click on " + webObject.Name);
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to click on an element. Element information: {webObject.Name}";
                BaseTest.Node.Fail(message);
                throw exception;
            }
        }

        public static void EnterText(WebObject webObject, string text)
        {
            try
            {
                var element = WaitForElementToBeVisible(webObject);
                element.Clear();
                element.SendKeys(text);
                Console.WriteLine(text + " is entered in the " + webObject.Name + " field.");
                BaseTest.Node.Pass(text + " is entered in the " + webObject.Name + " field.");
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to enter text to a field. Element information: {webObject.Name}";
                BaseTest.Node.Fail(message);
                throw exception;
            }
        }

        public static void MoveToElement(WebObject webObject)
        {
            try
            {
                var element = WaitForElementToBeClickable(webObject);
                new Actions(BaseTest.GetWebDriver())
                   .MoveToElement(element)
                   .Perform();
                Console.WriteLine("Scroll to " + webObject.Name);
                BaseTest.Node.Pass("Scroll to " + webObject.Name);
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to move to an element. Element information: {webObject.Name}";
                BaseTest.Node.Fail(message);
                throw exception;
            }
        }
    }
}