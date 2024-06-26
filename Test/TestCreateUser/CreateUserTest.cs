﻿using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Extenstions;
using AssetManagement.Library;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Library.Utils;
using AssetManagement.Pages;


namespace AssetManagement.Test.TestCreateUser
{
    public class CreateUserTest : BaseTest
    {
        private LoginPage _loginPage;
        private HomePage _homePage;
        private ManageUserPage _manageUserPage;
        private CreateNewUserPage _createNewUserPage;

        [SetUp]
        public void PageSetUp()
        {
            _loginPage = new LoginPage();
            _homePage = new HomePage();
            _manageUserPage = new ManageUserPage();
            _createNewUserPage = new CreateNewUserPage();
        }

        [Test, Description("Create Admin user successfully")]
        [TestCase("valid_admin")]
        public void CreateAdminUserSuccessfully(string accountKey)
        {
            Account valid_user = AccountData[accountKey];
            User createdUser = UserDataProvider.CreateRandomValidUser();

            ExtentReportHelper.LogTestStep("Go to Login page.");
            BrowserFactory.WebDriver.Url = loginUrl;

            ExtentReportHelper.LogTestStep("Login");
            _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Go to Manage User page");
            _homePage.NavigateToManageUserPage();

            ExtentReportHelper.LogTestStep("Go to Create User page");
            _manageUserPage.GoToCreateUserPage();

            ExtentReportHelper.LogTestStep("Create new Admin user with valid data");
            _createNewUserPage.CreateNewUser(createdUser);

            ExtentReportHelper.LogTestStep("Verify user information");
            _manageUserPage.VerifyUserInformation(createdUser);
            _manageUserPage.CloseModal();
            _manageUserPage.StoreDataToDisable();
        }

        [TearDown]
        public void AfterCreateUserTest()
        {
            _manageUserPage.DeleteCreatedUserFromStorage();
        }
    }
}