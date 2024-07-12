using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;
using AssetManagement.Pages.AssetPage;
using AssetManagement.Pages.AssignmentPage;
using AssetManagement.Updater;
using FluentAssertions;
using RazorEngine.Compilation.ImpromptuInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Test.AssetTest
{
    public class DeleteAssetTest : BaseTest
    {
        private ManageAssetPage _manageAssetPage;
        private Asset createdAsset;

        [SetUp]
        public void BeforeDeleteAssetTest()
        {
            createdAsset = AssetDataProvider.CreateRandomValidAsset();
        }

        [Test, Description("Delete asset successfully")]
        [TestCase("valid_admin")]
        public void DeleteAssetSuccessfully(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Go to Manage Asset page");
            _manageAssetPage = _homePage.NavigateToMangeAssetPage();

            ExtentReportHelper.LogTestStep("Create new Asset with valid data");
            CreateNewAssetPage _createNewAssetPage = _manageAssetPage.GoToCreateAssetPage();
            _createNewAssetPage.CreateNewAsset(createdAsset);

            ExtentReportHelper.LogTestStep("Delete the created Asset");
            string createdAssetCode = _manageAssetPage.GetAssetCodeOfCreatedAsset();
            _manageAssetPage.DeleteAsset(createdAssetCode);

            ExtentReportHelper.LogTestStep("Verify message");
            _manageAssetPage.VerifyMessage(MessageConstant.DeleteAssetSuccesfullyMessage);

            ExtentReportHelper.LogTestStep("Verify that delete Asset successfully");
            _manageAssetPage.EnterSearchKeyword(createdAssetCode);
            _manageAssetPage.IsAssetExist(createdAssetCode).Should().BeFalse();
        }
    }
}
