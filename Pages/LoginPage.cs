using AssetManagement.DataObjects;
using AssetManagement.Library;
using OpenQA.Selenium;


namespace AssetManagement.Pages
{
    public class LoginPage 
    {
        //Web ELements
        private Element _userNameInput = new Element(By.Id("username"));
        private Element _passwordInput = new Element(By.Id("password"));
        private Element _loginBtn = new Element(By.XPath("//button[text()='Login']"));


        //Page Method
        public void InputUserName(string username)
        {
            _userNameInput.ClearText();
            _userNameInput.InputText(username);
        }

        public void InputPassword(string password)
        {
            _passwordInput.ClearText();
            _passwordInput.InputText(password);
        }

        public void ClickOnLoginBtn()
        {
            _loginBtn.ClickOnElement();
        }

        public void Login(Account account)
        {
            InputUserName(account.UserName);
            InputPassword(account.Password);
            ClickOnLoginBtn();
        }


    }

}
