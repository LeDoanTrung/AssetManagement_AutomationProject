using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Pages
{
    public class HomePage : BasePage
    {
        //Web Element
        
        //Method
        public void NavigateToManageUserPage(string menuItem ="Manage User")
        {
            MenuTab.SelectMenuItem(menuItem);
        }

        public void NavigateToMangeAssetPage(string menuItem = "Manage Asset")
        {
            MenuTab.SelectMenuItem(menuItem);
        }

        public void NavigateToMangeAssignmentPage(string menuItem = "Manage Assignment")
        {
            MenuTab.SelectMenuItem(menuItem);
        }

        public void NavigateToRequestReturningPage(string menuItem = "Request for Returning")
        {
            MenuTab.SelectMenuItem(menuItem);
        }

        public void NavigateToReportPage(string menuItem = "Report")
        {
            MenuTab.SelectMenuItem(menuItem);
        }

        public void IsAtHomePage()
        {
            userName_value.IsElementDisplayed().Should().BeTrue();
        }
    }
}
