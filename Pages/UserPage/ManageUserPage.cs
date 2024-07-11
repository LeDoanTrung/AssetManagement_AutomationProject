using AssetManagement.DataObjects;
using FluentAssertions;
using OpenQA.Selenium;
using AssetManagement.Library.ShareData;
using AssetManagement.Library;


namespace AssetManagement.Pages.UserPage
{
    public class ManageUserPage : BasePage
    {
        //Web Element
        private Element _createUserBtn = new Element(By.XPath("//button[text()='Create New User']"));
        private Element _searchBar = new Element(By.Id("search-input"));
        private Element _searchIcon = new Element(By.Id("search-button"));
        private Element _nextButton = new Element(By.XPath("//span[text()='Next']"));
        private Element _noRowsFoundMessage = new Element(By.XPath("//h4[text()=' No User Found']"));
        private Element _closeModalIcon = new Element(By.Id("close-modal-button"));
        private Element _disableButtonModal = new Element(By.XPath("//button[.='Yes']"));
        private string _modalFieldTitle = "//div[contains(@class,'modal-field')]";
        private string _modalValue = "//div[contains(@class,'modal-value')]";
        private string _headerLocator = "#table-header th";
        private string _tableRow = "#table tbody tr";
        private string _cellLocator = "#table tbody td";
        private string _disableIconLocator = "svg[data-icon='circle-xmark']";
        private string _editIconLocator = "svg[data-icon='pencil']";
        private Element _userRow(string staffCode)
        {
            return new Element(By.XPath($"//td[.='{staffCode}']/.."));
        }

        //Method
        public CreateNewUserPage GoToCreateUserPage()
        {
            _createUserBtn.Click();
            return new CreateNewUserPage();
        }
        public EditUserPage GoToEditUser(string staffCode)
        {
            var editIcon = _userRow(staffCode).FindElement(By.CssSelector(_editIconLocator));
            editIcon.Click();
            return new EditUserPage();
        }

        public void EnterSearchKeyword(string keyword)
        {
            _searchBar.ClearText();
            _searchBar.InputText(keyword);
            _searchIcon.Click();
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

        public void OpenDetailCreatedRecord()
        {
            string staffCode = GetStaffCodeOfCreatedUser();
            _userRow(staffCode).Click();
        }

        public void CloseModal()
        {
            _closeModalIcon.Click();
        }

        public void VerifyCreatedUser(User user)
        {
            WaitForLoading(); // Wait for loading data
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

        public void VerifyUserInformation(User user)
        {
            OpenDetailCreatedRecord();
            VerifyCreatedUser(user);
        }

        public bool VerifySearchUserWithAssociatedResult(string keyword)
        {
            WaitForLoading(); // Wait for loading data
            int staffCodeIndex = FindIndexOfHeaderColumn("Staff Code");
            int fullNameIndex = FindIndexOfHeaderColumn("Full Name");

            bool allRowsContainKeyword = true;

            if (staffCodeIndex == -1 || fullNameIndex == -1)
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

                    // Check if any of the columns contain the keyword
                    if (!IsKeywordInText(keyword, staffCode) && !IsKeywordInText(keyword, fullName))
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

        public bool IsUserEnabled(string code)
        {
            return _userRow(code).IsElementExist();
        }
        public void DisableUser(string staffCode)
        {
            if (IsUserEnabled(staffCode))
            {
                var disableIcon = _userRow(staffCode).FindElement(By.CssSelector(_disableIconLocator));
                disableIcon.Click();
                _disableButtonModal.Click();
            }
        }

        public string GetStaffCodeOfCreatedUser()
        {
            int staffCodeIndex = FindIndexOfHeaderColumn("Staff Code");
            var cells = BrowserFactory.WebDriver.FindElements(By.CssSelector(_cellLocator));
            string staffCode = cells.ElementAt(staffCodeIndex).Text;
            return staffCode;
        }
        public void StoreDataToDisable()
        {
            string staffCode = GetStaffCodeOfCreatedUser();

            DataStorage.SetData("hasCreatedUser", true);
            DataStorage.SetData("staffCode", staffCode);
        }

        public void DisableCreatedUserFromStorage()
        {
            if ((bool)DataStorage.GetData("hasCreatedUser"))
            {
                DisableUser(
                (string)DataStorage.GetData("staffCode")
                );
            }
        }

        public void FindAndDisableUserAfterTest()
        {
            EnterSearchKeyword((string)DataStorage.GetData("staffCode"));
            DisableCreatedUserFromStorage();
        }
    }
}
