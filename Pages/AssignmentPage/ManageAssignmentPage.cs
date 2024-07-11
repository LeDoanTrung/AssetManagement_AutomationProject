using AssetManagement.DataObjects;
using AssetManagement.Library;
using AssetManagement.Library.ShareData;
using AssetManagement.Pages.AssetPage;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Pages.AssignmentPage
{
    public class ManageAssignmentPage : BasePage
    {
        //Web Element
        private Element _createAssignmentBtn = new Element(By.XPath("//button[text()='Create new assignment']"));
        private Element _searchBar = new Element(By.Id("search-input"));
        private Element _searchIcon = new Element(By.Id("search-button"));
        private Element _nextButton = new Element(By.XPath("//span[text()='Next']"));
        private Element _closeModalIcon = new Element(By.Id("close-modal-button"));
        private Element _deleteButtonModal = new Element(By.XPath("//button[.='Yes']"));
        private string _headerLocator = "#table-header th";
        private string _tableRow = "#table tbody tr";
        private string _cellLocator = "#table tbody td";
        private string _deleteIconLocator = "svg[data-icon='circle-xmark']";
        private string _editIconLocator = "svg[data-icon='pencil']";
        private string _showMoreLocator = "//span[text()=' Show more']";
        private Element _modalData(string field) 
        {
            return new Element(By.CssSelector($"div[id=assignment_model_row_data_{field}]"));
        }
        private Element _assignmentRow(string assignmentId)
        {
            return new Element(By.XPath($"//td[.='{assignmentId}']/.."));
        }
        private Element _selectedRow(string assetName, string status)
        {
            return new Element(By.XPath($"//div[text()='{status}']/ancestor::tr/descendant::div[text()='{assetName}']"));
        }

        //Method
        public CreateNewAssignmentPage GoToCreateAssignmentPage()
        {
            _createAssignmentBtn.Click();
            return new CreateNewAssignmentPage();
        }

        public string GetIdOfCreatedAssignment()
        {
            WaitForLoading();
            int assignmentIdIndex = FindIndexOfHeaderColumn("No.");
            var cells = BrowserFactory.WebDriver.FindElements(By.CssSelector(_cellLocator));
            string assignmentId = cells.ElementAt(assignmentIdIndex).Text;
            return assignmentId;
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

         public void OpenDetailCreatedRecord(string assetName)
        {
            _selectedRow(assetName, "Waiting for acceptance").Click();
        }

        public void CloseModal()
        {
            _closeModalIcon.ClickWithScroll();
        }

        public void VerifyCreatedAssignment(Assignment assignment, Account assignedToAccount, Account assignedByAccount , Asset asset)
        {
            string assetName = _modalData("assetName").GetText();
            string specification = _modalData("specification").GetText().Replace(" Show less", "");
            string assignedTo = _modalData("assignedTo").GetText();
            string assignedBy = _modalData("assignedBy").GetText();
            string assignedDate = _modalData("assignedDate").GetText();
            string state = _modalData("status").GetText();
            string note = _modalData("note").GetText().Replace(" Show less", "");

            assetName.Should().Be(asset.Name, "Asset name does not match.");
            specification.Should().Be(asset.Specification, "Specification does not match.");
            assignedTo.Should().Be(assignedToAccount.UserName);
            assignedBy.Should().Be(assignedByAccount.UserName);
            assignedDate.Should().Be(assignment.AssignedDate);
            state.Should().Be("Waiting for acceptance", "State does not match.");
            note.Should().Be(assignment.Note);
        }

        public void VerifyAssignmentInformation(Assignment assignment, Account assignedToAccount, Account assignedByAccount , Asset asset)
        {
            OpenDetailCreatedRecord(asset.Name);
            ClickAllShowMoreButtons();
            VerifyCreatedAssignment(assignment, assignedToAccount, assignedByAccount, asset);
        }

        public bool VerifySearchAssignmentWithAssociatedResult(string keyword)
        {
            WaitForLoading(); //Waiting for loading data
            int assetCodeIndex = FindIndexOfHeaderColumn("Asset Code");
            int assetNameIndex = FindIndexOfHeaderColumn("Asset Name");
            int assignedToIndex = FindIndexOfHeaderColumn("Assigned To");

            bool allRowsContainKeyword = true;

            if (assetCodeIndex == -1 || assetNameIndex == -1 || assignedToIndex == -1)
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
                    string assignedTo = cells.ElementAt(assignedToIndex).Text;

                    // Check if any of the columns contain the keyword
                    if (!IsKeywordInText(keyword, assetCode) && !IsKeywordInText(keyword, assetName) && !IsKeywordInText(keyword, assignedTo))
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
                _nextButton.ClickWithScroll();
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

        public bool IsAssignmentExist(string id)
        {
            return _assignmentRow(id).IsElementExist();
        }

        public void DeleteAssignment(string assignmentId)
        {
            if (IsAssignmentExist(assignmentId))
            {
                var deleteIcon = _assignmentRow(assignmentId).FindElement(By.CssSelector(_deleteIconLocator));
                deleteIcon.Click();
                _deleteButtonModal.Click();
            }           
        }

        public void StoreDataToDelete()
        {
            string assignmentId = GetIdOfCreatedAssignment();

            DataStorage.SetData("hasCreatedAsset", true);
            DataStorage.SetData("assignmentId", assignmentId);
        }

        public void DeleteCreatedAssignmentFromStorage()
        {
            if ((bool)DataStorage.GetData("hasCreatedAsset"))
            {
                DeleteAssignment(
                (string)DataStorage.GetData("assignmentId")
                );
            }
        }

    }
}
