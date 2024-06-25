using AssetManagement.Constants;
using AssetManagement.DataObjects;
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
        private Dictionary<string, User> UserData;

        [SetUp]
        public void PageSetUp()
        {
            _loginPage = new LoginPage();
            _homePage = new HomePage();
            _manageUserPage = new ManageUserPage();
            UserData = JsonHelper.ReadAndParse<Dictionary<string, User>>(FileConstant.UserFilePath.GetAbsolutePath());
        }

        [Test, Description("Create Admin user successfully")]
        [TestCase("valid_account", "admin_user")]
        public void CreateAdminUserSuccessfully(string accountKey, string userKey)
        {
            Account valid_user = AccountData[accountKey];
            User createdUser = UserData[userKey];

            ExtentReportHelper.LogTestStep("Go to Login page.");
            BrowserFactory.WebDriver.Url = loginUrl;

            ExtentReportHelper.LogTestStep("Login");
            _loginPage.Login(valid_user);

            _homePage.NavigateToManageUserPage();

            _manageUserPage.CreateNewUser(createdUser);

            _manageUserPage.VerifyCreateUserSuccessfully(createdUser);
        }
    }
}
