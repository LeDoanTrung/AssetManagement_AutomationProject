using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;
using AssetManagement.Pages.UserPage;
using AssetManagement.Updater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Test.UserTest
{
    public class EditUserTest : BaseTest
    {
        private ManageUserPage _manageUserPage;
        private User beforeEditUser;
        private User afterEditUser;

        [SetUp]
        public void BeforeEditUserTest()
        {
            beforeEditUser = UserDataProvider.CreateRandomValidUser();
            afterEditUser = UserDataProvider.CreateRandomValidUser();
        }

        [Test, Description("Edit user successfully")]
        [TestCase("valid_admin")]
        public void EditUserWithValidDataSuccessfully(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Go to Manage User page");
            _manageUserPage = _homePage.NavigateToManageUserPage();

            ExtentReportHelper.LogTestStep("Go to Create User page");
            CreateNewUserPage _createNewUserPage = _manageUserPage.GoToCreateUserPage();

            ExtentReportHelper.LogTestStep("Create new Admin user with valid data");
            _createNewUserPage.CreateNewUser(beforeEditUser);

            ExtentReportHelper.LogTestStep("Edit the created user");
            string staffCode = _manageUserPage.GetStaffCodeOfCreatedUser();
            EditUserPage _editUserPage = _manageUserPage.GoToEditUser(staffCode);
            _editUserPage.EditNewUser(afterEditUser);
            User expectedUser = UserUpdater.CreateExpectedUser(beforeEditUser, afterEditUser);

            ExtentReportHelper.LogTestStep("Verify user information");
            _manageUserPage.VerifyUserInformation(expectedUser);
            _manageUserPage.CloseModal();
            _manageUserPage.StoreDataToDisable();
        }

        [TearDown]
        public void AfterEditUserTest()
        {
            _manageUserPage.DisableCreatedUserFromStorage();
        }
    }
}
