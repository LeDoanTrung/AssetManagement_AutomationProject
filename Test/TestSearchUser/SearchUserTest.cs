using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.Extenstions;
using AssetManagement.Library;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Library.Utils;
using AssetManagement.Pages;


namespace AssetManagement.Test.TestSearchUser
{
    public class SearchUserTest : BaseTest
    {
        private LoginPage _loginPage;
        private HomePage _homePage;
        private ManageUserPage _manageUserPage;
        private Dictionary<string, User> UserData;
        private Dictionary<string, SearchKeyword> KeyworData;

        [SetUp]
        public void PageSetUp()
        {
            _loginPage = new LoginPage();
            _homePage = new HomePage();
            _manageUserPage = new ManageUserPage();
            UserData = JsonHelper.ReadAndParse<Dictionary<string, User>>(FileConstant.UserFilePath.GetAbsolutePath());
            KeyworData = JsonHelper.ReadAndParse<Dictionary<string, SearchKeyword>>(FileConstant.KeywordFilePath.GetAbsolutePath());
        }

        [Test, Description("Search user by name")]
        [TestCase("valid_account", "admin_user", "user_name")]
        public void SearchUserByNameWithAssociatedResult(string accountKey, string userKey, string keywordKey)
        {
            Account valid_user = AccountData[accountKey];
            User createdUser = UserData[userKey];
            SearchKeyword searchKeyword = KeyworData[keywordKey];

            ExtentReportHelper.LogTestStep("Go to Login page.");
            BrowserFactory.WebDriver.Url = loginUrl;

            ExtentReportHelper.LogTestStep("Login");
            _loginPage.Login(valid_user);

            _homePage.NavigateToManageUserPage();

            _manageUserPage.EnterSearchKeyword(searchKeyword.Keyword);
            _manageUserPage.VerifySearchUserWithAssociatedResultSuccessfully(searchKeyword.Keyword);
        }
    }
}
