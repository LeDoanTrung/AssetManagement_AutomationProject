using AssetManagement.Library;
using OpenQA.Selenium;

namespace AssetManagement.Pages
{
    public class MenuTab
    {
        private Element _menuItem(string itemName)
        {
            return new Element(By.XPath($"//li[.='{itemName}']"));
        } 

        public void SelectMenuItem(string itemName)
        {
            _menuItem(itemName).ClickOnElement();
        }

        public Element GetMenuItem(string itemName)
        {
            return _menuItem(itemName);
        }
    }
}
