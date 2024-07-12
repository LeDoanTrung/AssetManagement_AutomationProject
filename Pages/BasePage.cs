using AssetManagement.Library;
using AssetManagement.Pages.AssetPage;
using AssetManagement.Pages.AssignmentPage;
using AssetManagement.Pages.RequestForReturningPage;
using AssetManagement.Pages.UserPage;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;


namespace AssetManagement.Pages
{
    public abstract class BasePage
    {
        //Web Element
        protected Element _userName_value = new Element(By.Id("username"));
        protected Element _navbar_title = new Element(By.Id("navbar-title"));
        protected Element _loadingIcon = new Element(By.CssSelector("div[class='loader']"));
        protected MenuTab _menuTab;
        protected Element _message(string message)
        {
            return new Element(By.XPath($"//span[text()='{message}']"));
        }

        public BasePage() 
        {
            _menuTab = new MenuTab();
        }

        public bool VerifyMessage(string message)
        {
            return _message(message).IsElementDisplayed();
        }

        public void WaitForLoading()
        {
            _loadingIcon.WaitForElementToDisappear();
        }

        public void WaitForMessageDissapear(string message)
        {
            _message(message).WaitForElementToDisappear();
        }

        public void Wait(int milliseconds)
        {
            Task.Delay(milliseconds).Wait();
        }

        public HomePage NavigateToManageHomePage(string menuItem = "Home")
        {
            _menuTab.SelectMenuItem(menuItem);
            return new HomePage();
        }

        public ManageUserPage NavigateToManageUserPage(string menuItem = "Manage User")
        {
            _menuTab.SelectMenuItem(menuItem);
            return new ManageUserPage();
        }

        public ManageAssetPage NavigateToMangeAssetPage(string menuItem = "Manage Asset")
        {
            _menuTab.SelectMenuItem(menuItem);
            return new ManageAssetPage();
        }

        public ManageAssignmentPage NavigateToMangeAssignmentPage(string menuItem = "Manage Assignment")
        {
            _menuTab.SelectMenuItem(menuItem);
            return new ManageAssignmentPage();
        }

        public RequestReturningPage NavigateToRequestReturningPage(string menuItem = "Request for Returning")
        {
            _menuTab.SelectMenuItem(menuItem);
            return new RequestReturningPage();
        }

    }
}
