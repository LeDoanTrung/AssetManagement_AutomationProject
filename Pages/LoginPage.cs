using AssetManagement.DataObjects;
using AssetManagement.Library;
using OpenQA.Selenium;


namespace AssetManagement.Pages
{
    public class LoginPage 
    {
        //Web ELements
        private Element userNameInput = new Element(By.Id("username"));
        private Element passwordInput = new Element(By.Id("password"));
        private Element loginBtn = new Element(By.XPath("//button[text()='Login']"));


        //Page Method
        public void InputUserName(string username)
        {
            userNameInput.ClearText();
            userNameInput.InputText(username);
        }

        public void InputPassword(string password)
        {
            passwordInput.ClearText();
            passwordInput.InputText(password);
        }

        public void ClickOnLoginBtn()
        {
            loginBtn.ClickOnElement();
        }

        public void Login(Account account)
        {
            InputUserName(account.userName);
            InputPassword(account.password);
            ClickOnLoginBtn();
        }


    }

}
