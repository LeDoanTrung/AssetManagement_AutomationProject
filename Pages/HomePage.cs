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
        //Method
        public void IsAtHomePage()
        {
            _userName_value.IsElementDisplayed().Should().BeTrue();
        }

        public void VerifyAdminHomePage()
        {
            _menuTab.GetMenuItem("Home").IsElementDisplayed().Should().BeTrue();
            _menuTab.GetMenuItem("Manage User").IsElementDisplayed().Should().BeTrue();
            _menuTab.GetMenuItem("Manage Asset").IsElementDisplayed().Should().BeTrue();
            _menuTab.GetMenuItem("Manage Assignment").IsElementDisplayed().Should().BeTrue();
            _menuTab.GetMenuItem("Request for Returning").IsElementDisplayed().Should().BeTrue();
            _menuTab.GetMenuItem("Report").IsElementDisplayed().Should().BeTrue();
        }

        public void VerifyStaffHomePage()
        {
            _menuTab.GetMenuItem("Home").IsElementDisplayed().Should().BeTrue();
            _menuTab.GetMenuItem("Manage User").IsElementExist().Should().BeFalse();
            _menuTab.GetMenuItem("Manage Asset").IsElementExist().Should().BeFalse();
            _menuTab.GetMenuItem("Manage Assignment").IsElementExist().Should().BeFalse();
            _menuTab.GetMenuItem("Request for Returning").IsElementExist().Should().BeFalse();
            _menuTab.GetMenuItem("Report").IsElementExist().Should().BeFalse();
        }
    }
}
