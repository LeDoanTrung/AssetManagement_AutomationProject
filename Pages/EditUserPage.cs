using AssetManagement.DataObjects;
using AssetManagement.Extenstions;
using AssetManagement.Library;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Pages
{
    public class EditUserPage : BasePage
    {
        //Web Element
        private Element _dateOfBirth = new Element(By.Id("dateOfBirth"));
        private Element _joinedDate = new Element(By.Id("joinedDate"));
        private string _genderLocator = "//label[text()='{0}']";
        private Element _typeDropdown = new Element(By.Id("roleId"));
        private Element _saveButton = new Element(By.XPath("//button[text()='Save']"));

        //Method
        public void InputDateOfBirth(string date)
        {
            string formattedDate = StringExtensions.ConvertDateFormat(date, "dd/MM/yyyy", "MM/dd/yyyy");
            _dateOfBirth.SendKeys(formattedDate);
        }
        public void InputJoinedDate(string date)
        {
            string formattedDate = StringExtensions.ConvertDateFormat(date, "dd/MM/yyyy", "MM/dd/yyyy");
            _joinedDate.SendKeys(formattedDate);
        }

        public void SelectGender(string gender)
        {
            Element genderValue = this.DynamicElement(_genderLocator, gender);
            genderValue.ClickOnElement();
        }

        public void SelectType(string type)
        {
            _typeDropdown.SelectOptionByText(type);
        }

        public void ClickOnSaveBtn()
        {
            _saveButton.IsElementEnabled();
            _saveButton.ClickOnElement();
        }

        public void EditNewUser(User user)
        {
            InputDateOfBirth(user.DateOfBirth);
            SelectGender(user.Gender);
            InputJoinedDate(user.JoinedDate);
            SelectType(user.Type);
            ClickOnSaveBtn();
        }
    }
}
