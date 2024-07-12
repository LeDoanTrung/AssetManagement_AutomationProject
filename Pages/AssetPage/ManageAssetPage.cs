using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.Library;
using AssetManagement.Library.ShareData;
using AventStack.ExtentReports;
using Bogus.DataSets;
using FluentAssertions;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Pages.AssetPage
{
    public class ManageAssetPage : BasePage
    {
        //Web Element
        private Element _createAssetBtn = new Element(By.XPath("//button[text()='Create New Asset']"));
        private Element _searchBar = new Element(By.Id("search-input"));
        private Element _searchIcon = new Element(By.Id("search-button"));
        private Element _nextButton = new Element(By.XPath("//span[text()='Next']"));
        private Element _closeModalIcon = new Element(By.Id("close-modal-button"));
        private Element _deleteButtonModal = new Element(By.XPath("//button[.='Yes']"));
        private string _headerLocator = "#table-header th";
        private string _tableRow = "#table tbody tr";
        private string _cellLocator = "#table tbody td";
        private string _showMoreLocator = "//span[text()=' Show more']";
        private Element _modalData(string field)
        {
            return new Element(By.XPath($"//div[text()='{field}']/following-sibling::div[@name='asset_modal_row_data']"));
        }
        private Element _assetRow(string assetCode)
        {
            return new Element(By.XPath($"//td[.='{assetCode}']/.."));
        }
        private Element _editIcon(string assetCode)
        {
            return new Element(By.XPath($"//td[.='{assetCode}']/..//button[@name='table_icon_pencil']"));
        }
        private Element _deleteIcon(string assetCode)
        {
            return new Element(By.XPath($"//td[.='{assetCode}']/..//button[@name='table_icon_circle-xmark']"));
        }
        //Method
        public CreateNewAssetPage GoToCreateAssetPage()
        {
            _createAssetBtn.Click();
            return new CreateNewAssetPage();
        }

        public EditAssetPage GoToEditAsset(string assetCode)
        {
            _editIcon(assetCode).Click();
            return new EditAssetPage(); 
        }

        public string GetAssetCodeOfCreatedAsset()
        {
            WaitForLoading(); // Wait for fetching data
            int assetCodeIndex = FindIndexOfHeaderColumn("Asset Code");
            var cells = BrowserFactory.WebDriver.FindElements(By.CssSelector(_cellLocator));
            string assetCode = cells.ElementAt(assetCodeIndex).Text;
            return assetCode;
        }
        public void EnterSearchKeyword(string keyword)
        {
            _searchBar.ClearText();
            _searchBar.InputText(keyword);
            _searchIcon.Click();
        }

        public void ClickAllShowMoreButtons()
        {
            var showMoreElements = BrowserFactory.WebDriver.FindElements(By.XPath(_showMoreLocator));

            foreach (var element in showMoreElements)
            {
                element.Click();
            }
        }

        public int FindIndexOfHeaderColumn(string headerName)
        {
            // Get all column header elements
            var headerElements = BrowserFactory.WebDriver.FindElements(By.CssSelector(_headerLocator));

            for (int i = 0; i < headerElements.Count; i++)
            {

                if (headerElements[i].Text.Equals(headerName, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }

        public void OpenDetailCreatedRecord()
        {
            string assetCode = GetAssetCodeOfCreatedAsset();
            _assetRow(assetCode).Click();
        }

        public void CloseModal()
        {
            _closeModalIcon.Click();
        }

        public void VerifyCreatedAsset(Asset asset)
        {          
            string assetCode = _modalData("Asset Code").GetText();
            string assetName = _modalData("Asset Name").GetText();
            string category = _modalData("Category").GetText();
            string installedDate = _modalData("Installed Date").GetText();
            string state = _modalData("State").GetText();
            string specification = _modalData("Specification").GetText().Replace(" Show less", "");

            AssetCodeVerifier(assetCode, asset.Category).Should().BeTrue();
            assetName.Should().Be(asset.Name, "Asset name does not match.");
            category.Should().Be(asset.Category, "Category does not match.");
            installedDate.Should().Be(asset.InstalledDate, "Installed Date does not match.");
            state.Should().Be(asset.State, "State does not match.");
            specification.Should().Be(asset.Specification, "Specification does not match.");
        }

        public bool AssetCodeVerifier(string assetCode, string category)
        {
            if (string.IsNullOrEmpty(assetCode) || string.IsNullOrEmpty(category))
            {
                return false;
            }
            //Verify that assetCode contains correct prefix
            string[] categoryWords = category.Trim().Split(' ');
            string categoryInitials = string.Concat(categoryWords.Select(word => word[0]));
            return assetCode.StartsWith(categoryInitials, StringComparison.OrdinalIgnoreCase);
        }

        public void VerifyAssetInformation(Asset asset)
        {
            OpenDetailCreatedRecord();
            ClickAllShowMoreButtons();
            VerifyCreatedAsset(asset);
        }

        public bool VerifySearchAssetWithAssociatedResult(string keyword)
        {
            WaitForLoading(); // Wait for loading data
            int assetCodeIndex = FindIndexOfHeaderColumn("Asset Code");
            int assetNameIndex = FindIndexOfHeaderColumn("Asset Name");

            bool allRowsContainKeyword = true;

            if (assetCodeIndex == -1 || assetNameIndex == -1 )
            {
                throw new Exception("One or more field of modal not found.");
            }
            do
            {
                var rows = BrowserFactory.WebDriver.FindElements(By.CssSelector(_tableRow));

                foreach (var row in rows)
                {
                    // Get cells in the row
                    var cells = row.FindElements(By.CssSelector(_cellLocator));

                    string assetCode = cells.ElementAt(assetCodeIndex).Text;
                    string assetName = cells.ElementAt(assetNameIndex).Text;

                    // Check if any of the columns contain the keyword
                    if (!IsKeywordInText(keyword, assetCode) && !IsKeywordInText(keyword, assetName))
                    {
                        allRowsContainKeyword = false;
                        break;
                    }

                }
                // Check if Next button is disabled
                if (_nextButton.IsDisabled())
                {
                    break;
                }
                _nextButton.Click();
            } while (true);

            if (allRowsContainKeyword)
            {
                return true;
            }

            return allRowsContainKeyword;
        }

        private bool IsKeywordInText(string keyword, string text)
        {
            return !string.IsNullOrWhiteSpace(text) && text.Contains(keyword, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsAssetExist(string code)
        {
            return _assetRow(code).IsElementExist();
        }

        public void DeleteAsset(string assetCode)
        {
            if (IsAssetExist(assetCode))
            {
                _deleteIcon(assetCode).Click();
                _deleteButtonModal.Click();
            }

        }

        public void StoreDataToDelete()
        {
            string assetCode = GetAssetCodeOfCreatedAsset();

            DataStorage.SetData("hasCreatedAsset", true);
            DataStorage.SetData("assetCode", assetCode);
        }

        public void DeleteCreatedAssetFromStorage()
        {
            if ((bool)DataStorage.GetData("hasCreatedAsset"))
            {
                DeleteAsset(
                (string)DataStorage.GetData("assetCode")
                );
            }
        }

    }
}
