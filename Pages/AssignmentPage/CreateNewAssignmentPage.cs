using AssetManagement.DataObjects;
using AssetManagement.Extenstions;
using AssetManagement.Library;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AssetManagement.Pages.AssignmentPage
{
    public class CreateNewAssignmentPage : BasePage
    {
        //Web Element
        private Element _userField = new Element(By.Id("user_field"));
        private Element _assetField = new Element(By.Id("asset_field"));
        private Element _assignedDate = new Element(By.Id("assignedDate_field"));
        private Element _noteField = new Element(By.Id("note_field"));
        private Element _saveButton = new Element(By.Id("save"));
        private Element _modalSaveButton(string field)
        {
            return new Element(By.XPath($"//div[.='{field}']/ancestor::div[@class='row']/following-sibling::div//button[text()='Save']"));
        }
        private Element _searchBar(string field) //User List or Asset List
        {
            return new Element(By.XPath($"//div[.='{field}']/descendant::input"));
        }
        private Element _searchIcon(string field) //User List or Asset List
        {
            return new Element(By.XPath($"//div[.='{field}']/descendant::button"));
        }

        private Element _selectUserRow(string name) 
        {
            return new Element(By.XPath($"//td[.='{name}']/../descendant::input"));
        }


        //Method
        public void InputUser(string userName, string field = "User List")
        {
            _userField.Click();
            WaitForLoading(); // Wait for loading search result
            _searchBar(field).ClearText();
            _searchBar(field).InputText(userName);
            _searchIcon(field).Click();
            _selectUserRow(userName).ClickWithScroll();
            _modalSaveButton(field).ClickWithScroll();
        }

        public void InputAsset(string assetName, string field = "Asset List")
        {
            _assetField.Click();
            WaitForLoading(); // Wait for loading search result
            _searchBar(field).ClearText();
            _searchBar(field).InputText(assetName);
            _searchIcon(field).Click();
            _selectUserRow(assetName).Click();
            _modalSaveButton(field).ClickWithScroll();
        }

        public void InputAssignedDate(string date)
        {
            string formattedDate = StringExtensions.ConvertDateFormat(date, "dd/MM/yyyy", "MM/dd/yyyy");
            _assignedDate.SendKeys(formattedDate);
        }

        public void InputNote(string note)
        {
            _noteField.ClearText();
            _noteField.InputText(note);
        }

        public void ClickOnSaveBtn()
        {
            _saveButton.Click();
        }

        public void CreateNewAssignment(Assignment assignment, string userName, string assetName)
        {
            InputUser(userName);
            InputAsset(assetName);
            InputAssignedDate(assignment.AssignedDate);
            InputNote(assignment.Note);
            ClickOnSaveBtn();
        }
    }
}
