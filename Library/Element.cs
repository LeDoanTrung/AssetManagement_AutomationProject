﻿using OpenQA.Selenium;
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

        public void ClickWithScroll()
        {
            IWebElement element = WaitForElementToBeClickable();
            ScrollHelper.ScrollToElement(BrowserFactory.WebDriver, this);
            element.Click();
        }

        public void Click()
        {
            IWebElement element = WaitForElementToBeClickable();
            element.Click();
        }
        public void ClearText()
        {
            var element = WaitForElementToVisible();
            element.Clear();
        }

        public void ClearTextFromTextArea()
        {
            var element = WaitForElementToVisible();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
        }

        public void InputText(string text)
        {
            var element = WaitForElementToVisible();
            element.SendKeys(text);
        }

        public string GetText()
        {
            var text = WaitForElementToVisible().Text;
            return text;
        }

        public void SendKeys(string keys)
        {
            var element = WaitForElementToVisible();
            element.SendKeys(keys);
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
        public void SelectOptionByText(string option)
        {
            IWebElement element = BrowserFactory.WebDriver.FindElement(this.By);
            SelectElement select = new SelectElement(element);
            select.SelectByText(option);
        }

        public bool IsDisabled()
        {
            IWebElement element = WaitForElementToVisible();
            return element.GetAttribute("class").Contains("disabled") != null;
        }
        public Element FindElement(By by)
        {
            var parentElement = WaitForElementToVisible();
            parentElement.FindElement(by);
            return new Element(by);
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

        public void WaitForElementToDisappear()
        {        
            if (!BrowserFactory.Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(this.By)))
            {
                throw new WebDriverTimeoutException($"Element with locator: {this.By} did not disappear");
            }
        }

    }
}
