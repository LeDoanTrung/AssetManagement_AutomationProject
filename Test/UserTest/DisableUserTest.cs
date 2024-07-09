using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Pages.AssetPage;
using AssetManagement.Pages.UserPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManagement.Library.ReportHelper;
using FluentAssertions;
using AssetManagement.Pages;

namespace AssetManagement.Test.UserTest
{
    public class DisableUserTest : BaseTest
    {
        private ManageUserPage _manageUserPage;
        private User createdUser;

        [SetUp]
        public void BeforeDisableUserTest()
        {
            createdUser = UserDataProvider.CreateRandomValidUser();
        }

        [Test, Description("Disable user successfully")]
        [TestCase("valid_admin")]
        public void DisableUserSuccessfully(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Go to Manage User page");
            _manageUserPage = _homePage.NavigateToManageUserPage();

            ExtentReportHelper.LogTestStep("Create new User with valid data");
            CreateNewUserPage _createNewAssetPage = _manageUserPage.GoToCreateUserPage();
            _createNewAssetPage.CreateNewUser(createdUser);

            ExtentReportHelper.LogTestStep("Delete the created User");
            string createdUserCode = _manageUserPage.GetStaffCodeOfCreatedUser();
            _manageUserPage.DisableUser(createdUserCode);

            ExtentReportHelper.LogTestStep("Verify that diasble User successfully");
            _manageUserPage.VerifyMessage(MessageConstant.DisableUserSuccessfullyMessage);
            _manageUserPage.EnterSearchKeyword(createdUserCode);
            _manageUserPage.IsUserEnabled(createdUserCode).Should().BeFalse();
        }
    }
}
