using AssetManagement.Constants;
using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Pages;
using AssetManagement.Pages.AssetPage;
using AssetManagement.Pages.AssignmentPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Test.AssignmentTest
{
    public class SearchAssignmentTest : BaseTest
    {
        private ManageAssignmentPage _manageAssignmentPage;
        private ManageAssetPage _manageAssetPage;
        private Asset createdAsset;
        private Assignment createdAssignment;

        [SetUp]
        public void BeforeSearchAssignmentTest()
        {
            createdAsset = AssetDataProvider.CreateRandomValidAssetForAssignment();
            createdAssignment = AssignmentDataProvider.CreateRandomValidAssignment();
        }

        [Test, Description("Search assignment by Assigned User")]
        [TestCase("valid_admin")]
        public void SearchAssignmentByAssignedToWithAssociatedResult(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Create new Asset with valid data for assignment");
            _manageAssetPage = _homePage.NavigateToMangeAssetPage();
            CreateNewAssetPage _createNewAssetPage = _manageAssetPage.GoToCreateAssetPage();
            _createNewAssetPage.CreateNewAsset(createdAsset);
            _manageAssetPage.WaitForMessageDissapear(MessageConstant.CreateAsssetSuccessfullyMessage);

            ExtentReportHelper.LogTestStep("Create new Assignment with valid data");
            _manageAssignmentPage = _homePage.NavigateToMangeAssignmentPage();
            CreateNewAssignmentPage _createNewAssignmentPage = _manageAssignmentPage.GoToCreateAssignmentPage();
            _createNewAssignmentPage.CreateNewAssignment(createdAssignment, valid_user.FullName, createdAsset.Name);

            ExtentReportHelper.LogTestStep("Search Assignment by Assigned User's name");
            _manageAssignmentPage.EnterSearchKeyword(createdAsset.Name);

            ExtentReportHelper.LogTestStep("Verify search result");
            _manageAssignmentPage.VerifySearchAssignmentWithAssociatedResult(createdAsset.Name);

            _manageAssetPage.StoreDataToDelete();
        }

        [TearDown]
        public void AfterSearchAssignmentTest()
        {
            _manageAssignmentPage.DeleteCreatedAssignmentFromStorage();
        }
    }
}
