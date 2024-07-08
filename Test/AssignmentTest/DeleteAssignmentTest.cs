using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;
using AssetManagement.Pages.AssetPage;
using AssetManagement.Pages.AssignmentPage;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Test.AssignmentTest
{
    public class DeleteAssignmentTest : BaseTest
    {
        private ManageAssignmentPage _manageAssignmentPage;
        private ManageAssetPage _manageAssetPage;
        private Asset createdAsset;
        private Assignment createdAssignment;

        [SetUp]
        public void BeforeDeleteAssignmentTest()
        {
            createdAsset = AssetDataProvider.CreateRandomValidAssetForAssignment();
            createdAssignment = AssignmentDataProvider.CreateRandomValidAssignment();
        }

        [Test, Description("Delete assignment successfully")]
        [TestCase("valid_admin")]
        public void DeleteAssignmentSuccesffully(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Create new Asset with valid data for assignment");
            _manageAssetPage = _homePage.NavigateToMangeAssetPage();
            CreateNewAssetPage _createNewAssetPage = _manageAssetPage.GoToCreateAssetPage();
            _createNewAssetPage.CreateNewAsset(createdAsset);

            ExtentReportHelper.LogTestStep("Create new Assignment with valid data");
            _manageAssignmentPage = _homePage.NavigateToMangeAssignmentPage();
            CreateNewAssignmentPage _createNewAssignmentPage = _manageAssignmentPage.GoToCreateAssignmentPage();
            _createNewAssignmentPage.CreateNewAssignment(createdAssignment, valid_user.FullName, createdAsset.Name);

            ExtentReportHelper.LogTestStep("Delete Assignment with valid data");
            string assignmentId = _manageAssignmentPage.GetIdOfCreatedAssignment();
            _manageAssignmentPage.DeleteAssignment(assignmentId);

            ExtentReportHelper.LogTestStep("Verify that delete assignment successfully");
            _manageAssignmentPage.VerifyMessage(MessageConstant.DeleteAssignmentSuccesfullyMessage);
            _manageAssignmentPage.EnterSearchKeyword(assignmentId);
            _manageAssignmentPage.IsAssignmentExist(assignmentId).Should().BeFalse();
        }
    }
}
