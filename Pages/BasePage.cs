using AssetManagement.Library;
using AssetManagement.Pages.AssetPage;
using AssetManagement.Pages.AssignmentPage;
using AssetManagement.Pages.UserPage;
using OpenQA.Selenium;


namespace AssetManagement.Pages
{
    public abstract class BasePage
    {
        //Web Element
        protected Element _userName_value = new Element(By.Id("username"));
        protected Element _navbar_title = new Element(By.Id("navbar-title"));
        protected MenuTab _menuTab;

        public BasePage() 
        {
            _menuTab = new MenuTab();
        }
        public void Wait(int milliseconds)
        {
            Task.Delay(milliseconds).Wait();
        }

        public void RefreshPage()
        {
            BrowserFactory.WebDriver.Navigate().Refresh();
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
            Wait(2000); // Wait for loading data
            _menuTab.SelectMenuItem(menuItem);
            return new ManageAssignmentPage();
        }

        public RequestReturningPage NavigateToRequestReturningPage(string menuItem = "Request for Returning")
        {
            _menuTab.SelectMenuItem(menuItem);
            return new RequestReturningPage();
        }

        public ReportPage NavigateToReportPage(string menuItem = "Report")
        {
            _menuTab.SelectMenuItem(menuItem);
            return new ReportPage();
        }
    }
}
