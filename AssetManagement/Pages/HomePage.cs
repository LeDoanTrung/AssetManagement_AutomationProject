using OpenQA.Selenium;
using AssetManagement.Library;
using AssetManagement.Test;

namespace AssetManagement.Pages
{
    public class HomePage
    {
        //Web Elements
        private WebObject _loginLink = new WebObject(By.ClassName("ico-login"), "Login Link");

        //Contructor
        public HomePage() { }

        //Page Methods
        public void ClickLoginLink()
        {
            DriverUtils.ClickOnElement(_loginLink);
        }

        public void VisitHomePage()
        {
            DriverUtils.GoToUrl(ConfigurationHelper.GetConfigurationByKey("TestURL"));
        }
    }
}