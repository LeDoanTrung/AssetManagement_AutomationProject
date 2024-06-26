using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Test.TestEditUser
{
    public class EditUserTest : BaseTest
    {
        private LoginPage _loginPage;
        private HomePage _homePage;
        private ManageUserPage _manageUserPage;
        private CreateNewUserPage _createNewUserPage;
        private EditUserPage _editUserPage;


        [SetUp]
        public void PageSetUp()
        {
            _loginPage = new LoginPage();
            _homePage = new HomePage();
            _manageUserPage = new ManageUserPage();
            _createNewUserPage = new CreateNewUserPage();
            _editUserPage = new EditUserPage();
        }

        [Test, Description("Edit user successfully")]
        [TestCase("valid_admin")]
        public void EditUserSuccessfully(string accountKey)
        {
            Account valid_user = AccountData[accountKey];
            User createdUser = UserDataProvider.CreateRandomValidUser();
            User edittedUser = UserDataProvider.CreateRandomValidUser();

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

            ExtentReportHelper.LogTestStep("Go to edit user page");
            _manageUserPage.GoToEditUser();

            ExtentReportHelper.LogTestStep("Edit user with valid data");
            _editUserPage.EditNewUser(edittedUser);
            User expectedUser = User.CreateExpectedUser(createdUser, edittedUser);

            ExtentReportHelper.LogTestStep("Verify user information");
            _manageUserPage.VerifyUserInformation(expectedUser);
            _manageUserPage.CloseModal();
            _manageUserPage.StoreDataToDisable();
        }

        [TearDown]
        public void AfterEditUserTest()
        {
            _manageUserPage.DeleteCreatedUserFromStorage();
        }
    }
}
