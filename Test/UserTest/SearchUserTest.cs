using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;
using AssetManagement.Pages.UserPage;


namespace AssetManagement.Test.UserTest
{
    public class SearchUserTest : BaseTest
    {
        private ManageUserPage _manageUserPage;

        [Test, Description("Search user by name")]
        [TestCase("valid_admin")]
        public void SearchUserByNameWithAssociatedResult(string accountKey)
        {
            Account valid_user = AccountData[accountKey];
            User createdUser = UserDataProvider.CreateRandomValidUser();

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Go to Manage User page");
            _manageUserPage = _homePage.NavigateToManageUserPage();

            ExtentReportHelper.LogTestStep("Create new user for searching");
            CreateNewUserPage _createNewUserPage = _manageUserPage.GoToCreateUserPage();
            _createNewUserPage.CreateNewUser(createdUser);

            ExtentReportHelper.LogTestStep("Search user by name");
            _manageUserPage.EnterSearchKeyword(createdUser.FirstName);

            ExtentReportHelper.LogTestStep("Verify search result");
            _manageUserPage.VerifySearchUserWithAssociatedResult(createdUser.FirstName);

            _manageUserPage.StoreDataToDisable();
        }

        [TearDown]
        public void AfterSearchUserTest()
        {
            _manageUserPage.DeleteCreatedUserFromStorage();
        }
    }
}
