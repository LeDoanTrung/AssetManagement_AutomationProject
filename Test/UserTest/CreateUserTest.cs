using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Library.Utils;
using AssetManagement.Pages;
using AssetManagement.Pages.UserPage;


namespace AssetManagement.Test.TestCreateUser
{
    public class CreateUserTest : BaseTest
    {
        private ManageUserPage _manageUserPage;

        [Test, Description("Create Admin user successfully")]
        [TestCase("valid_admin")]
        public void CreateAdminUserSuccessfully(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);
            User createdUser = UserDataProvider.CreateRandomValidUser();

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Go to Manage User page");
            _manageUserPage = _homePage.NavigateToManageUserPage();

            ExtentReportHelper.LogTestStep("Go to Create User page");
            CreateNewUserPage _createNewUserPage = _manageUserPage.GoToCreateUserPage();

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
            _manageUserPage.DisableCreatedUserFromStorage();
        }
    }
}
