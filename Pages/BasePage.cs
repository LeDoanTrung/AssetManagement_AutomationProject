using AssetManagement.Library;
using OpenQA.Selenium;


namespace AssetManagement.Pages
{
    public abstract class BasePage
    {
        //Web Element
        protected Element userName_value = new Element(By.Id("username"));


        //Method
        protected Element DynamicElement(string locator, params string[] parameters)
        {
            string formattedLocator = String.Format(locator, parameters);
            Element element = new Element(By.XPath(formattedLocator));
            return element;
        }

        protected void SelectElementWithDynamicLocator(string locator, params string[] parameters)
        {
            DynamicElement(locator, parameters).ClickOnElement();
        }

        protected string SelectTextWithDynamicLocator(string locator, params string[] parameters)
        {
            return DynamicElement(locator, parameters).GetText();
        }

        protected void Wait(int milliseconds)
        {
            Task.Delay(milliseconds).Wait();
        }
    }
}
