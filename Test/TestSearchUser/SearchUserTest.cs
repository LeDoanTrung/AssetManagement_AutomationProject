using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;


namespace AssetManagement.Test.TestSearchUser
{
    public class SearchUserTest : BaseTest
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

        [Test, Description("Search user by name")]
        [TestCase("valid_admin")]
        public void SearchUserByNameWithAssociatedResult(string accountKey)
        {
            Account valid_user = AccountData[accountKey];
            User createdUser = UserDataProvider.CreateRandomValidUser();

            ExtentReportHelper.LogTestStep("Go to Login page.");
            BrowserFactory.WebDriver.Url = loginUrl; 

            ExtentReportHelper.LogTestStep("Login");
            _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Go to Manage User page");
            _homePage.NavigateToManageUserPage();

            ExtentReportHelper.LogTestStep("Create new user for searching");
            _manageUserPage.GoToCreateUserPage();
            _createNewUserPage.CreateNewUser(createdUser);

            ExtentReportHelper.LogTestStep("Search User by name");
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
