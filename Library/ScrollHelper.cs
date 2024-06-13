using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Library
{
    public class ScrollHelper
    {
        public static void ScrollToElement(IWebDriver driver, Element element)
        {
            try
            {
                // Scroll to element using JavaScript
                IWebElement webElement = element.GetWebElement(driver);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", webElement);


                // Additional action to ensure the element is in view
                Actions actions = new Actions(driver);
                actions.MoveToElement(webElement).Perform();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Scrolling to element failed: " + ex.Message);
            }
        }
    }
}
