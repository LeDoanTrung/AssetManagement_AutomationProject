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
        private Element _assignmentRow(string assetCode)
        {
            return new Element(By.XPath($"//td[.='{assetCode}']/.."));
        }
        private Element _acceptIcon(string assetCode)
        {
            return new Element(By.XPath($"//td[.='{assetCode}']/..//button[@name='table_icon_check']"));
        }
        private Element _returnIcon(string assetCode)
        {
            return new Element(By.XPath($"//td[.='{assetCode}']/..//button[@name='table_icon_rotate-left']"));
        }
        private Element _yesBtn = new Element(By.XPath("//button[.='Yes']"));


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

        public bool IsAssignmentExist(string assetCode)
        {
            return _assignmentRow(assetCode).IsElementExist();
        }
        public void RequestForReturnAsset(string assetCode)
        {
            _returnIcon(assetCode).ClickWithScroll();
            _yesBtn.Click();
        }        

        public void AcceptAssignment(string assetCode)
        {
            if (IsAssignmentExist(assetCode))
            {
                _acceptIcon(assetCode).Click();
                _yesBtn.Click();
            }
        }


    }
}
