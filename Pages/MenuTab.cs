using AssetManagement.Library;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Pages
{
    public static class MenuTab
    {
        public static void SelectMenuItem(string itemName)
        {
            Element menuItem = new Element(By.XPath($"//li[.='{itemName}']"));
            menuItem.ClickOnElement();
        }
    }
}
