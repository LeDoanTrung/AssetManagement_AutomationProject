using AssetManagement.DataObjects;
using AssetManagement.Extenstions;
using AssetManagement.Library;
using OpenQA.Selenium;


namespace AssetManagement.Pages.UserPage
{
    public class EditUserPage : BasePage
    {
        //Web Element
        private Element _dateOfBirth = new Element(By.Id("dateOfBirth"));
        private Element _joinedDate = new Element(By.Id("joinedDate"));
        private Element _typeDropdown = new Element(By.Id("roleId"));
        private Element _saveButton = new Element(By.XPath("//button[text()='Save']"));
        private Element _yesButton = new Element(By.XPath("//button[text()='Yes']"));
        private Element _gender(string gender)
        {
            return new Element(By.XPath($"//label[text()='{gender}']"));
        }


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
            _gender(gender).ClickWithScroll();
        }

        public void SelectType(string type)
        {
            _typeDropdown.SelectOptionByText(type);
        }

        public void ClickOnSaveBtn()
        {
            _saveButton.IsElementEnabled();
            _saveButton.ClickWithScroll();
        }

        public void ClickOnYesBtn()
        {
            _yesButton.Click();
        }

        public void EditNewUser(User user)
        {
            InputDateOfBirth(user.DateOfBirth);
            SelectGender(user.Gender);
            InputJoinedDate(user.JoinedDate);
            SelectType(user.Type);
            ClickOnSaveBtn();
            ClickOnYesBtn();
        }
    }
}
