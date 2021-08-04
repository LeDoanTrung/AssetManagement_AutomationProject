using OpenQA.Selenium;
using AssetManagement.Library;

namespace AssetManagement.Pages
{
    public class LoginPage
    {
        //Web Elements
        private WebObject _emailTextbox = new WebObject(By.Id("Email"), "Email Textbox");
        private WebObject _password_Textbox = new WebObject(By.Id("Password"), "Password Textbox");
        private WebObject _loginButton = new WebObject(By.XPath("//button[contains(text(),'Log in')]"), "Log in Button");
        private WebObject _errorMessageLabel = new WebObject(By.XPath("//div[@class = 'message-error validation-summary-errors']"), "Error Messsage Label");

        //Contructor
        public LoginPage() { }

        //Page Methods
        public void Login(string email, string password)
        {
            DriverUtils.EnterText(_emailTextbox, email);
            DriverUtils.EnterText(_password_Textbox, password);
            DriverUtils.ClickOnElement(_loginButton);
        }

        public string GetMessageErrorText()
        {
            return DriverUtils.GetTextFromElement(_errorMessageLabel);
        }
    }
}