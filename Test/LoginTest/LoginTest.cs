using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.Library;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;

namespace AssetManagement.Test.TestLogin
{
    public class LoginTest : BaseTest
    {

        [Test, Description("Admin user login successfully with valid Username and Password")]
        [TestCase("valid_admin")]
        public void LoginWithValidAdminAccountSuccessfully(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Verify is at Homepage");
            _homePage.VerifyMessage(MessageConstant.LoginSuccessfullyMessage);

            ExtentReportHelper.LogTestStep("Verify is at Homepage");
            _homePage.IsAtHomePage();
            _homePage.VerifyAdminHomePage();
        }

        [Test, Description("Staff user login successfully with valid Username and Password")]
        [TestCase("valid_staff")]
        public void LoginWithValidStaffAccountSuccessfully(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Verify is at Homepage");
            _homePage.VerifyMessage(MessageConstant.LoginSuccessfullyMessage);

            ExtentReportHelper.LogTestStep("Verify is at Homepage");
            _homePage.IsAtHomePage();
            _homePage.VerifyStaffHomePage();
        }
    }
}
