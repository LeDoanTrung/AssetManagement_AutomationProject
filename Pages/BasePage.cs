using AssetManagement.Library;
using FluentAssertions;
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
        //Web Element
        protected Element userName_value = new Element(By.Id("username"));
        protected Element DynamicElement(string locator, params string[] parameters)
        {
            string formattedLocator = String.Format(locator, parameters);
            Element element = new Element(By.XPath(formattedLocator));
            return element;
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
