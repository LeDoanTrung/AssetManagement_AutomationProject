﻿using AssetManagement.DataObjects;
using AssetManagement.Library;
using AssetManagement.Library.Util;
using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Pages.UserPage
{
    public class CreateNewUserPage : BasePage
    {
        //Web Element
        private Element _firstNameTextBox = new Element(By.Id("firstName"));
        private Element _lastNameTextBox = new Element(By.Id("lastName"));
        private Element _dateOfBirth = new Element(By.Id("dateOfBirth"));
        private Element _joinedDate = new Element(By.Id("joinedDate"));
        private Element _typeDropdown = new Element(By.Id("roleId"));
        private Element _staffTypeDropdown = new Element(By.Id("type"));
        public Element _locationDropdown = new Element(By.Id("location"));
        private Element _saveButton = new Element(By.XPath("//button[text()='Save']"));
        private Element _gender(string gender)
        {
            return new Element(By.XPath($"//label[text()='{gender}']"));
        }

        //Method

        public void InputFirstName(string firstName)
        {
            _firstNameTextBox.ClearText();
            _firstNameTextBox.InputText(firstName);
        }

        public void InputLastName(string lastName)
        {
            _lastNameTextBox.ClearText();
            _lastNameTextBox.InputText(lastName);
        }

        public void InputDateOfBirth(string date)
        {
            string formattedDate = StringUtility.ConvertDateFormat(date, "dd/MM/yyyy", "MM/dd/yyyy");
            _dateOfBirth.SendKeys(formattedDate);
        }
        public void InputJoinedDate(string date)
        {
            string formattedDate = StringUtility.ConvertDateFormat(date, "dd/MM/yyyy", "MM/dd/yyyy");
            _joinedDate.SendKeys(formattedDate);
        }

        public void SelectGender(string gender)
        {
            _gender(gender).ClickWithScroll();
        }

        public void SelectType(string type)
        {
            _typeDropdown.SelectOptionByText(type);
        }

        public void SelectStaffType(string staffType)
        {
            _staffTypeDropdown.SelectOptionByText(staffType);
        }

        public void SelectLocation(string location)
        {
            _locationDropdown.SelectOptionByText(location);
        }
        public void ClickOnSaveBtn()
        {
            _saveButton.IsElementEnabled();
            _saveButton.ClickWithScroll();
        }
        public void CreateNewUser(User user)
        {
            InputFirstName(user.FirstName);
            InputLastName(user.LastName);
            InputDateOfBirth(user.DateOfBirth);
            SelectGender(user.Gender);
            InputJoinedDate(user.JoinedDate);
            SelectType(user.Type);
            SelectStaffType(user.StaffType);
            if (user.Type.Equals("Admin"))
            {
                SelectLocation(user.Location);
            }
            ClickOnSaveBtn();
        }

    }
}
