using AssetManagement.DataObjects;
using AssetManagement.Library;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;


namespace AssetManagement.Test.TestLogin
{
    public class LoginTest : BaseTest
    {
        private LoginPage _loginPage;
        private HomePage _homePage;

        [SetUp]
        public void PageSetUp()
        {
            _loginPage = new LoginPage();
            _homePage = new HomePage();
        }

        [Test, Description("Admin user login successfully with valid Username and Password")]
        [TestCase("valid_admin")]
        public void LoginWithValidAdminAccountSuccessfully(string accountKey)
        {
            Account valid_user = AccountData[accountKey];
            ExtentReportHelper.LogTestStep("Go to Login page.");
            BrowserFactory.WebDriver.Url = loginUrl;

            ExtentReportHelper.LogTestStep("Login");
            _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Verify is at Homepage");
            _homePage.IsAtHomePage();
        }

        [Test, Description("Staff user login successfully with valid Username and Password")]
        [TestCase("valid_staff")]
        public void LoginWithValidStaffAccountSuccessfully(string accountKey)
        {
            Account valid_user = AccountData[accountKey];
            ExtentReportHelper.LogTestStep("Go to Login page.");
            BrowserFactory.WebDriver.Url = loginUrl;

            ExtentReportHelper.LogTestStep("Login");
            _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Verify is at Homepage");
           _homePage.IsAtHomePage();
        }
    }
}
