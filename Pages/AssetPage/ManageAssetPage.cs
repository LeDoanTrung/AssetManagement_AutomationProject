using AssetManagement.DataObjects;
using AssetManagement.Library;
using AssetManagement.Library.ShareData;
using Bogus.DataSets;
using FluentAssertions;
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
        private Element _firstAssetRecord = new Element(By.CssSelector("#table tbody tr:first-child"));
        private Element _nextButton = new Element(By.XPath("//span[text()='Next']"));
        private Element _noRowsFoundMessage = new Element(By.XPath("//h4[text()=' No Asset Found']"));
        private Element _closeModalIcon = new Element(By.Id("close-modal-button"));
        private Element _deleteButtonModal = new Element(By.XPath("//button[.='Delete']"));
        private string _modalFieldTitle = "div[name = 'asset_modal_row_header']";
        private string _modalValue = "div[name = 'asset_modal_row_data']";
        private string _headerLocator = "#table-header th";
        private string _tableRow = "#table tbody tr";
        private string _cellLocator = "#table tbody td";
        private string _deleteIconLocator = "svg[data-icon='circle-xmark']";
        private string _editIconLocator = "svg[data-icon='pencil']";
        private string _showMoreLocator = "//span[text()=' Show more']";
        private Element _assetRow(string staffCode)
        {
            return new Element(By.XPath($"//td[.='{staffCode}']/.."));
        }

        //Method
        public CreateNewAssetPage GoToCreateAssetPage()
        {
            _createAssetBtn.ClickOnElement();
            return new CreateNewAssetPage();
        }

        public EditAssetPage GoToEditAsset()
        {
            string assetCode = GetAssetCodeOfCreatedAsset();
            var editIcon = _assetRow(assetCode).FindElement(By.CssSelector(_editIconLocator));
            editIcon.ClickOnElement();
            return new EditAssetPage(); 
        }

        public string GetAssetCodeOfCreatedAsset()
        {
            Wait(1000);
            int assetCodeIndex = FindIndexOfHeaderColumn("Asset Code");
            var cells = BrowserFactory.WebDriver.FindElements(By.CssSelector(_cellLocator));
            string assetCode = cells.ElementAt(assetCodeIndex).Text;
            return assetCode;
        }
        public void EnterSearchKeyword(string keyword)
        {
            _searchBar.ClearText();
            _searchBar.InputText(keyword);
            _searchIcon.ClickOnElement();
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

        public int FindIndexOfTitleModal(string title)
        {
            var tileElements = BrowserFactory.WebDriver.FindElements(By.CssSelector(_modalFieldTitle));

            for (int i = 0; i < tileElements.Count; i++)
            {

                if (tileElements[i].Text.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }

        public void OpenDetailFirstRecort()
        {
            _firstAssetRecord.ClickOnElement();
        }

        public void CloseModal()
        {
            _closeModalIcon.ClickOnElement();
        }


        public void VerifyCreatedAsset(Asset asset)
        {
            ClickAllShowMoreButtons();
            int assetCodeIndex = FindIndexOfTitleModal("Asset Code");
            int assetNameIndex = FindIndexOfTitleModal("Asset Name");
            int categoryIndex = FindIndexOfTitleModal("Category");
            int installedDateIndex = FindIndexOfTitleModal("Installed Date");
            int stateIndex = FindIndexOfTitleModal("State");
            int specificationIndex = FindIndexOfTitleModal("Specification");

            if (assetCodeIndex == -1 || assetNameIndex == -1 || categoryIndex == -1 || installedDateIndex == -1 ||
                stateIndex == -1 || specificationIndex == -1)
            {
                throw new Exception("One or more field of modal not found.");
            }

            var valueElements = BrowserFactory.WebDriver.FindElements(By.CssSelector(_modalValue));

            string assetCode = valueElements[assetCodeIndex].Text;
            string assetName = valueElements[assetNameIndex].Text;
            string category = valueElements[categoryIndex].Text;
            string installedDate = valueElements[installedDateIndex].Text;
            string state = valueElements[stateIndex].Text;
            string specification = valueElements[specificationIndex].Text.Replace(" Show less", "");


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
            OpenDetailFirstRecort();
            VerifyCreatedAsset(asset);
        }

        public bool VerifySearchAssetWithAssociatedResult(string keyword)
        {
            Wait(1000);
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
                _nextButton.ClickOnElement();
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

        public void DeleteAsset(string assetCode)
        {
            var deleteIcon = _assetRow(assetCode).FindElement(By.CssSelector(_deleteIconLocator));
            deleteIcon.ClickOnElement();
            _deleteButtonModal.ClickOnElement();
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
