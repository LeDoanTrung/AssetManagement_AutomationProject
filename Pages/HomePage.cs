using AssetManagement.DataObjects;
using AssetManagement.Library;
using FluentAssertions;
using OpenQA.Selenium;
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
        //Element
        private string _returnIconLocator = "svg[data-icon='rotate-left']";
        private string _acceptIconLocator = "svg[data-icon='check']";
        private Element _assignmentRow(string assetCode)
        {
            return new Element(By.XPath($"//td[.='{assetCode}']/.."));
        }
        private Element _buttonOnModal(string value)
        {
            return new Element(By.XPath($"//button[.='{value}']"));
        }

        //Method
        public void IsAtHomePage()
        {
            _userName_value.IsElementDisplayed().Should().BeTrue();
        }

        public void VerifyAdminHomePage()
        {
            _menuTab.GetMenuItem("Home").IsElementExist().Should().BeTrue();
            _menuTab.GetMenuItem("Manage User").IsElementExist().Should().BeTrue();
            _menuTab.GetMenuItem("Manage Asset").IsElementExist().Should().BeTrue();
            _menuTab.GetMenuItem("Manage Assignment").IsElementExist().Should().BeTrue();
            _menuTab.GetMenuItem("Request for Returning").IsElementExist().Should().BeTrue();
            _menuTab.GetMenuItem("Report").IsElementExist().Should().BeTrue();
        }

        public void VerifyStaffHomePage()
        {
            _menuTab.GetMenuItem("Home").IsElementExist().Should().BeTrue();
            _menuTab.GetMenuItem("Manage User").IsElementExist().Should().BeFalse();
            _menuTab.GetMenuItem("Manage Asset").IsElementExist().Should().BeFalse();
            _menuTab.GetMenuItem("Manage Assignment").IsElementExist().Should().BeFalse();
            _menuTab.GetMenuItem("Request for Returning").IsElementExist().Should().BeFalse();
            _menuTab.GetMenuItem("Report").IsElementExist().Should().BeFalse();
        }

        public bool IsAssignmentExist(string assetName)
        {
            return _assignmentRow(assetName).IsElementExist();
        }
        public void RequestForReturnAsset(string assetName)
        {
            if (IsAssignmentExist(assetName))
            {
                var returnIcon = _assignmentRow(assetName).FindElement(By.CssSelector(_returnIconLocator));
                returnIcon.Click();
                _buttonOnModal("Yes").Click();
            }
        }

        public void AcceptAssignment(string assetName)
        {
            if (IsAssignmentExist(assetName))
            {
                var acceptIcon = _assignmentRow(assetName).FindElement(By.CssSelector(_acceptIconLocator));
                acceptIcon.Click();
                _buttonOnModal("Yes").Click();
            }
        }


    }
}
