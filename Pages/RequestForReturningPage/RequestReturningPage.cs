using AssetManagement.Library;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Pages.RequestForReturningPage
{
    public class RequestReturningPage : BasePage
    {
        //Element
        private string _acceptIconLocator = "svg[data-icon='check']";
        private Element _searchBar = new Element(By.Id("search-input"));
        private Element _searchIcon = new Element(By.Id("search-button"));
        private Element _requestRow(string assetCode)
        {
            return new Element(By.XPath($"//td[.='{assetCode}']/.."));
        }
        private Element _buttonOnModal(string value)
        {
            return new Element(By.XPath($"//button[.='{value}']"));
        }

        private Element _cellOfRow(string value)
        {
            return new Element(By.XPath($"//td[.='{value}']"));
        }

        public bool IsRequestExist(string assetCode)
        {
            return _requestRow(assetCode).IsElementExist();
        }
        public void EnterSearchKeyword(string keyword)
        {
            _searchBar.ClearText();
            _searchBar.InputText(keyword);
            _searchIcon.Click();
        }

        public void CompleteTheRequest(string assetCode)
        {
            if (IsRequestExist(assetCode))
            {
                var acceptIcon = _requestRow(assetCode).FindElement(By.CssSelector(_acceptIconLocator));
                acceptIcon.Click();
                _buttonOnModal("Accept").Click();
            }
        }

        public void VerifyStatusOfRequest(string status)
        {
            _cellOfRow(status).IsElementExist().Should().BeTrue();
        }
    }
}
