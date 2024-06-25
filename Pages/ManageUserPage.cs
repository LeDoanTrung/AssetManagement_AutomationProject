using AssetManagement.DataObjects;
using AssetManagement.Library;
using FluentAssertions;
using AssetManagement.Extenstions;
using OpenQA.Selenium;
using System.Reflection.Metadata.Ecma335;


namespace AssetManagement.Pages
{
    public class ManageUserPage : BasePage
    {
        //Web Element
        private Element _createUserBtn = new Element(By.XPath("//button[text()='Create New User']"));
        private Element _searchBar = new Element(By.Id("search-input"));
        private Element _searchIcon = new Element(By.Id("search-button"));
        private Element _firstNameTextBox = new Element(By.Id("firstName"));
        private Element _lastNameTextBox = new Element(By.Id("lastName"));
        private Element _dateOfBirth = new Element(By.Id("dateOfBirth"));
        private Element _joinedDate = new Element(By.Id("joinedDate"));
        private string _genderLocator = "//label[text()='{0}']";
        private Element _typeDropdown = new Element(By.Id("roleId"));
        private Element _staffTypeDropdown = new Element(By.Id("type"));
        public Element _locationDropdown = new Element(By.Id("location"));
        private Element _saveButton = new Element(By.XPath("//button[text()='Save']"));
        private Element _firstUserRecord = new Element(By.CssSelector("tbody tr:first-child"));
        private string _headerLocator = "table th";
        private string _tableRow = "tbody tr";
        private string _cellLocator = "tbody td";
        private Element _nextButton = new Element(By.XPath("//span[text()='Next']"));
        private Element _noRowsFoundMessage = new Element(By.XPath("//h4[text()=' No User Found']"));
        private string _modalFieldTitle = "//div[@class='col-sm-4']/div";
        private string _modalValue = "//div[@class='col']/div";
        private Element _closeModalIcon = new Element(By.Id("close-modal-button"));


        //Method
        public void ClikcOnCreateUserBtn()
        {
            _createUserBtn.ClickOnElement();
        }

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
            _saveButton.ClickOnElement();
        }
        public void CreateNewUser(User user)
        {
            ClikcOnCreateUserBtn();
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

        public void EnterSearchKeyword(string keyword)
        {
            _searchBar.ClearText();
            _searchBar.InputText(keyword);
            _searchIcon.ClickOnElement();
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
            var tileElements = BrowserFactory.WebDriver.FindElements(By.XPath(_modalFieldTitle));

            for (int i = 0; i < tileElements.Count; i++)
            {

                if (tileElements[i].Text.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }
        
        public void VerifyCreateUserSuccessfully(User user)
        {
            _firstUserRecord.ClickOnElement();

            int staffCodeIndex = FindIndexOfTitleModal("Staff Code");
            int fullNameIndex = FindIndexOfTitleModal("Full Name");
            int dateOfBirthIndex = FindIndexOfTitleModal("Date of Birth");
            int genderIndex = FindIndexOfTitleModal("Gender");
            int joinedDateIndex = FindIndexOfTitleModal("Joined Date");
            int typeIndex = FindIndexOfTitleModal("Type");
            int locationIndex = FindIndexOfTitleModal("Location");

            if (staffCodeIndex == -1 || fullNameIndex == -1 || dateOfBirthIndex == -1 || genderIndex == -1 || 
                joinedDateIndex == -1 || typeIndex == -1 || locationIndex == -1)
            {
                throw new Exception("One or more field of modal not found.");
            }

            var valueElements = BrowserFactory.WebDriver.FindElements(By.XPath(_modalValue));

            string staffCode = valueElements[staffCodeIndex].Text;
            string fullName = valueElements[fullNameIndex].Text;
            string dateOfBirth = valueElements[dateOfBirthIndex].Text;
            string gender = valueElements[genderIndex].Text;
            string joinedDate = valueElements[joinedDateIndex].Text;
            string type = valueElements[typeIndex].Text;
            string location = valueElements[locationIndex].Text;

            staffCode.Should().Contain(user.StaffType, "Staff Code does not correct.");
            fullName.Should().Be($"{user.FirstName} {user.LastName}", "Full name does not match.");
            dateOfBirth.Should().Be(user.DateOfBirth, "Date of birth does not match.");
            gender.Should().Be(user.Gender, "Gender does not match.");
            joinedDate.Should().Be(user.JoinedDate, "Joined date does not match.");
            type.Should().Be(user.Type, "Type does not match.");
            location.Should().Be(user.Location.Split(':')[0].Trim(), "Location does not match.");
        }

        public bool VerifySearchUserWithAssociatedResultSuccessfully(string keyword)
        {
            Wait(2000);
            int staffCodeIndex = FindIndexOfHeaderColumn("Staff Code");
            int fullNameIndex = FindIndexOfHeaderColumn("Full Name");
            int usernameIndex = FindIndexOfHeaderColumn("Username");

            bool allRowsContainKeyword = true;

            if (staffCodeIndex == -1 || fullNameIndex == -1 || usernameIndex == -1)
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

                    string staffCode = cells.ElementAt(staffCodeIndex).Text;
                    string fullName = cells.ElementAt(fullNameIndex).Text;
                    string username = cells.ElementAt(usernameIndex).Text;

                    // Check if any of the columns contain the keyword
                    if (!IsKeywordInText(keyword, staffCode) && !IsKeywordInText(keyword, fullName) && !IsKeywordInText(keyword, username))
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

            if (_noRowsFoundMessage.IsElementDisplayed()) 
            {
                return true;
            }

            return allRowsContainKeyword;

        }

        private bool IsKeywordInText(string keyword, string text)
        {
            return !string.IsNullOrWhiteSpace(text) && text.Contains(keyword, StringComparison.OrdinalIgnoreCase);
        }


    }
}
