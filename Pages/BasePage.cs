using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Pages
{
    public abstract class BasePage
    {
        protected Element DynamicElement(string locator, string param)
        {
            string formattedLocator = String.Format(locator, param);
            Element element = new Element(By.XPath(formattedLocator));
            return element;
        }

        protected Element DynamicElement(string locator, string param1, string param2)
        {
            string formattedLocator = String.Format(locator, param1, param2);
            Element element = new Element(By.XPath(formattedLocator));
            return element;
        }
        protected void SelectElementWithDynamicLocator(string locator, string param)
        {
            DynamicElement(locator, param).ClickOnElement();
        }

        protected void SelectElementWithDynamicLocator(string locator, string param1, string param2)
        {
            DynamicElement(locator, param1, param2).ClickOnElement();
        }

        protected string SelectTextWithDynamicLocator(string locator, string param)
        {
            return DynamicElement(locator, param).GetText();
        }

    }
}
