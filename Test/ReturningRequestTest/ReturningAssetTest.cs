using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;
using AssetManagement.Pages.AssetPage;
using AssetManagement.Pages.AssignmentPage;
using AssetManagement.Pages.RequestForReturningPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Test.RequestForReturningTest
{
    public class ReturningAssetTest : BaseTest
    {
        private Asset createdAsset;
        private Assignment createdAssignment;

        [SetUp]
        public void BeforeReturningTest()
        {
            createdAsset = AssetDataProvider.CreateRandomValidAssetForAssignment();
            createdAssignment = AssignmentDataProvider.CreateRandomValidAssignmentForReturning();
        }

        [Test, Description("Return Asset successfully")]
        [TestCase("valid_admin")]
        public void AdminReturnHisAssetSuccessfully(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Go to Manage Asset page");
            ManageAssetPage _manageAssetPage = _homePage.NavigateToMangeAssetPage();
            CreateNewAssetPage _createNewAssetPage = _manageAssetPage.GoToCreateAssetPage();

            ExtentReportHelper.LogTestStep("Create new Asset with valid data for assignment");
            _createNewAssetPage.CreateNewAsset(createdAsset);
            string assetCode = _manageAssetPage.GetAssetCodeOfCreatedAsset();
            _manageAssetPage.WaitForMessageDissapear(MessageConstant.CreateAsssetSuccessfullyMessage);

            ExtentReportHelper.LogTestStep("Go to Manage Assignment page");
            ManageAssignmentPage _manageAssignmentPage = _homePage.NavigateToMangeAssignmentPage();
            CreateNewAssignmentPage _createNewAssignmentPage = _manageAssignmentPage.GoToCreateAssignmentPage();

            ExtentReportHelper.LogTestStep("Create new Assignment with valid data");
            _createNewAssignmentPage.CreateNewAssignment(createdAssignment, valid_user.FullName, createdAsset.Name);
            _manageAssignmentPage.WaitForMessageDissapear(MessageConstant.CreateAssignmentSuccessfullyMessage);

            ExtentReportHelper.LogTestStep("Accept the created assignment at the Homepage");
            _homePage.NavigateToManageHomePage();
            _homePage.AcceptAssignment(createdAsset.Name);

            ExtentReportHelper.LogTestStep("Return the asset at the Homepage");
            _homePage.RequestForReturnAsset(createdAsset.Name);

            ExtentReportHelper.LogTestStep("Go to Request for Returning page");
            RequestReturningPage _requestPage = _homePage.NavigateToRequestReturningPage();

            ExtentReportHelper.LogTestStep("Search and accept the request");
            _requestPage.EnterSearchKeyword(assetCode);
            _requestPage.CompleteTheRequest(assetCode);

            ExtentReportHelper.LogTestStep("Verify after completing the request");
            _requestPage.VerifyMessage(MessageConstant.AcceptRequestOfReturningMessage);
            _requestPage.VerifyStatusOfRequest("Completed");
        }
    }
}
