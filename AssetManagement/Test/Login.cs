using NUnit.Framework;
using AssetManagement.Pages;
using System;

namespace AssetManagement.Test
{
    public class Login : BaseTest
    {
        private HomePage _homePage = new HomePage();
        private LoginPage _loginPage = new LoginPage();

        [Test, Description("Log in with invalid email or password")]
        [Category("SmokeTest")]
        [Category("Regression")]
        public void LoginUnsucessfullyWithInvalidAccount()
        {
            Console.WriteLine("Test Login With Invalid email or password");
            _homePage.VisitHomePage();
            _homePage.ClickLoginLink();
            _loginPage.Login("Test1@Test123.com", "123456");
            Assert.That(_loginPage.GetMessageErrorText().Trim(), Is.EqualTo("Login was unsuccessful. Please correct the errors and try again.\r\nNo customer account found"));
        }

        [TestCase("Test@Test112.com", "12378"), Description("Log in with invalid email or password using parameter")]
        [TestCase("Test@Test112.com", "123456")]
        [Category("Regression")]
        public void LoginUnsucessfullyIfAccountIsInvalid(string email, string password)
        {
            Console.WriteLine("Test Login With Invalid email or password Using Param");
            _homePage.VisitHomePage();
            _homePage.ClickLoginLink();
            _loginPage.Login(email, password);
            Assert.That(_loginPage.GetMessageErrorText().Trim(), Is.EqualTo("Login was unsuccessful. Please correct the errors and try again.\r\nNo customer account found"));
        }
    }
}