using AssetManagement.DataObjects;
using AssetManagement.DataProvider;
using AssetManagement.Pages.AssetPage;
using AssetManagement.Pages.AssignmentPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManagement.Library.ReportHelper;
using AssetManagement.Constants;
using AssetManagement.Pages;

namespace AssetManagement.Test.AssignmentTest
{
    public class CreateAssignmentTest : BaseTest
    {
        private ManageAssignmentPage _manageAssignmentPage;
        private ManageAssetPage _manageAssetPage;

        [Test, Description("Create assignment successfully")]
        [TestCase("valid_admin")]
        public void AdminCreateNewAssignmentWithAllFieldsSuccessfullyForHisOwn(string accountKey)
        {
            Account valid_user = AccountData.GetAccount(accountKey);
            Asset createdAsset = AssetDataProvider.CreateRandomValidAssetForAssignment();
            Assignment createdAssignment = AssignmentDataProvider.CreateRandomValidAssignment();

            ExtentReportHelper.LogTestStep("Login");
            HomePage _homePage = _loginPage.Login(valid_user);

            ExtentReportHelper.LogTestStep("Create new Asset with valid data for assignment");
            _manageAssetPage = _homePage.NavigateToMangeAssetPage();
            CreateNewAssetPage _createNewAssetPage = _manageAssetPage.GoToCreateAssetPage();
            _createNewAssetPage.CreateNewAsset(createdAsset);
            _manageAssetPage.WaitForMessageDissapear(MessageConstant.CreateAsssetSuccessfullyMessage);

            ExtentReportHelper.LogTestStep("Go to Manage Assignment Page");
            _manageAssignmentPage = _homePage.NavigateToMangeAssignmentPage();

            ExtentReportHelper.LogTestStep("Go to Create Assignment Page");
            CreateNewAssignmentPage _createNewAssignmentPage = _manageAssignmentPage.GoToCreateAssignmentPage();

            ExtentReportHelper.LogTestStep("Create new Assignment with valid data");
            _createNewAssignmentPage.CreateNewAssignment(createdAssignment, valid_user.FullName, createdAsset.Name);

            ExtentReportHelper.LogTestStep("Verify Assignment information");
            _manageAssignmentPage.VerifyMessage(MessageConstant.CreateAssignmentSuccessfullyMessage);
            _manageAssignmentPage.VerifyAssignmentInformation(createdAssignment, valid_user, valid_user, createdAsset);
            _manageAssignmentPage.CloseModal();
            _manageAssignmentPage.StoreDataToDelete();
        }

        [TearDown]
        public void AfterCreateAssignmentTest()
        {
            _manageAssignmentPage.DeleteCreatedAssignmentFromStorage();
        }
    }
}
